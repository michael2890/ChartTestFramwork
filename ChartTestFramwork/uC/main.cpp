//--------------------------------------------------------------------------
// Titel         : Herzschlag KY-039 joy-it.net für ATmega8
//--------------------------------------------------------------------------
// Funktion      : Wandelt ein analoges Signal in ein digitales
// Schaltung     : Herzschlagsensor an PC.0, SPeaker an PB.0, AVCC, AGND, AVREF
//--------------------------------------------------------------------------
// Prozessor     : ATmega8
// Takt          : 3,6864 MHz
// Sprache       : Assembler
// Datum         : 15.08.2024
// Version       : 0.1
// Autor         : Christoph Töller
// Programmer	 : ...
// Port			 : ...
//--------------------------------------------------------------------------
#define F_CPU 3686400UL // 3.6864 MHz
#ifndef TIMER0_OVF_vect
// Für ältere WinAVR Versionen z.B. WinAVR-20071221 
#define TIMER0_OVF_vect TIMER0_OVF0_vect
#endif
#include <avr/io.h>
#include <inttypes.h>
#include <avr/interrupt.h>

#include <util/delay.h>
#include <string.h>
//--------------------------------------------------------------------------
unsigned char timer_value;
uint32_t timestamp = 0;

void UARTPutChar(uint8_t val);
void UARTPutDecimal(uint8_t byte);
void UARTPutBinary(uint8_t byte);
void UARTPutInt32(uint32_t zahl);
void UARTPutString(unsigned char*s)
{
	while(*s)
	{
		UARTPutChar(*s);
		s++;
	}
	UARTPutChar('\r');			//Fuer einen
	UARTPutChar('\n');			//Zeilenumbruch (Damit kann ReadLine beim Empfänger genutzt werden)
}

ISR(ADC_vect)
{
	int adc_value;
	//adc_value=ADC;
	adc_value=ADCH;
	
	timestamp++;
	
	//uint8_t t[1];
	//t[0]=';';
	
	//uint8_t s[1];
	//s[0]='\0';
	
	//UARTPutString(t);
	
	UARTPutInt32(timestamp);
	UARTPutChar(';');
	UARTPutDecimal(adc_value); //Wert zwischen 0 und 255 ohne vorher einen String zu schicken, sieht man das nicht in Putty...
	//UARTPutString(s);
	UARTPutChar('\r');			//Für einen Zeilenumbruch
	UARTPutChar('\n');			//
	
	if(adc_value<0x0F)
	{
		PORTB=0xFF;
	}
	else
	{
		PORTB=0x00;
	}
	
	//ADCSRA|=0b01000000;		//Start Conversion -> Durch Timer, um in definierten Zeitintervallen zu messen
}

void adcInit()
{
	ADMUX=0b01100000;				//Internal ref = ein, ADLAR=1 (Ergebnis in ADCH obersten 8Bit) ADC0 (PC0)
	//ADCSRA=0xDD;					//0b1101 1101 ADC Enable, ADC Start-Coversion, (No Freerunning),ADIF Clear
	ADCSRA=0xFD;					//			  Interrup Enable,  Prescaler -> 32
}

void timerInit()
{
	// Timer 0 konfigurieren
	// 0bxxxx x|CS02|CS01|CS00
	// 000 Stop
	// 001 CPU Takt
	// 010 CPU/8
	// 011 CPU/64
	// 100 CPU/256
	// 101 CPU/1024
	//TCCR0 = (1 << CS01); // Prescaler 8
	TCCR0 = 0b00000011; //Prescaler 64
	// Overflow Interrupt erlauben
	TIMSK |= (1 << TOIE0);

	/* Interrupt Aktion alle
	  Vorteiler 1    => 3.686.400Hz/256        = 14,4 kHz =>  69,44 us
	  Vorteiler 8    => (3.686.400Hz/8)/256    =  1,8 kHz => 555,55 us
	x Vorteiler 64   => (3.686.400Hz/64)/256   =  225  Hz =>   4,44 ms
	  Vorteiler 256  => (3.686.400Hz/256)/256  = 56,8  Hz =>  17,77 ms
	  Vorteiler 1024 => (3.686.400Hz/1024)/256 = 14,06 Hz =>  71,11 ms
	  (F_CPU=3686400/Prescaler)/256 Hz = f [Hz] bzw. 1/f => T
	*/

	/*Werteanzahl ca 2Tage: 2 * 24h * 60min * 60s * 225Werte = > 38.880.000
	    uint8_t 	8 Bit 	0 .. 255 	unsigned char
	    uint16_t 	16 Bit 	0 .. 65.535 	unsigned int
	  x uint32_t 	32 Bit 	0 .. 4.294.967.295 	unsigned long int
	*/
	timestamp = 0;
}
ISR(TIMER0_OVF_vect)
{
	ADCSRA |= 0b01000000;		//Start Conversion
}

void portsInit()
{
	DDRB=0xFF; //Port B ist Ausgang
}

void UARTInit()
{
	UBRRL=0x17;	// Baudrate 9600
	UCSRB=0x18;	// RXEN=1, TXEN=1
	UCSRC=0x86;	// Async. Mode, keine Parität
				// 8 Datenbit, 1 Stoppbit
}

uint8_t UARTGetChar ()
{
	while(!(UCSRA&(1<RXC)));	// warte auf Empfang
	return(UDR);				// empfangene Daten
}

void UARTPutChar(uint8_t val)
{
	while(!(UCSRA&(1<<UDRE)));	// warte auf freies Senderegister
	UDR=val;					// sende Daten
}

void UARTPutInt32(uint32_t zahl)		//Wird aktuell nicht genutzt
{
	UARTPutChar(0x30 + (zahl / 10000000));	// Zehnmilionen
	UARTPutChar(0x30 + ((zahl / 1000000) % 10));	// Milion
	UARTPutChar(0x30 + ((zahl / 100000)  % 10));	// Hunderttausender
	UARTPutChar(0x30 + ((zahl / 10000)   % 10));	// Zehntausender
	UARTPutChar(0x30 + ((zahl / 1000)    % 10));	// Tausender
	UARTPutChar(0x30 + ((zahl / 100)     % 10));	// Hunderter
	UARTPutChar(0x30 + ((zahl / 10)      % 10));	// Zehner
	UARTPutChar(0x30 + ((zahl % 10)));		// Einer
}

void UARTPutDecimal(uint8_t byte)		//Wird aktuell nicht genutzt
{
	UARTPutChar(0x30+ (byte/100));		// Hunderter
	UARTPutChar(0x30+ ((byte/10) %10));	// Zehner
	UARTPutChar(0x30+ (byte%10));		// Einer
}
void UARTPutBinary(uint8_t byte)		//wird aktuell nicht genutzt
{
	uint8_t bit;
	for(bit=7;bit<255;bit--)
	{
		if(byte& (1<<bit) )
		UARTPutChar('1');
		else
		UARTPutChar('0');
	}
}


main()
{
	//Initialisierung
	portsInit();			// PortB als Ausgang initialisieren
	adcInit();
	timerInit();
	sei();                  // enable interrupts
	
	UARTInit();
	//DDRC=0xff;
	//uint8_t z;
	uint8_t s[20];
	
	s[0]='r';
	s[1]='e';
	s[2]='a';
	s[3]='d';
	s[4]='y';
	s[5]='\0';
	
	while (!(UCSRA & (1<<RXC)))   // Warten auf Empfang
	{}
	
	UARTPutString(s);			//"ready" senden
	
	while(1)
	{
		while (!(UCSRA & (1<<RXC)))   // Warten auf Empfang
		{}
		
		UARTPutChar(UDR); //Empfangenes Zeichen als Quittung zurücksenden		
	}
}



