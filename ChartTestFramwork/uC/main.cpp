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
#include <avr/io.h>
#include <inttypes.h>
#include <avr/interrupt.h>

#include <util/delay.h>
#include <string.h>
//--------------------------------------------------------------------------
unsigned char timer_value;

void UARTPutChar(uint8_t val);
void UARTPutDecimal(uint8_t byte);
void UARTPutBinary(uint8_t byte);
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
	
	
	uint8_t s[1];
	
	s[0]='\0';
	//s[1]='e';
	//s[2]='s';
	//s[3]='\0';
	//s[4]='l';
	//s[5]='t';
	//s[6]='\0';
	/*
	UARTPutString(s);
	*/
	/*
	s[0]='b';
	s[1]='i';
	s[2]='n';
	s[3]='\0';
	s[4]='l';
	s[5]='t';
	s[6]='\0';
	UARTPutString(s);
	UARTPutBinary(adc_value);
	*/
	/*
	s[0]='d';
	s[1]='e';
	s[2]='c';
	s[3]='\0';
	s[4]='l';
	s[5]='t';
	s[6]='\0';
	UARTPutString(s);*/
	UARTPutDecimal(adc_value); //Wert zwischen 0 und 255 ohne vorher einen String zu schicken, sieht man das nicht in Putty...
	UARTPutString(s);
	
	if(adc_value<0x0F)
	{
		PORTB=0xFF;
	}
	else
	{
		PORTB=0x00;
	}
	
	ADCSRA|=0b01000000;		//Start Conversion
}

void adcInit()
{
	ADMUX=0b01100000;				//Internal ref = ein, ADLAR=1 (Ergebnis in ADCH obersten 8Bit) ADC0 (PC0)
	//ADCSRA=0xDD;			//0b1101 1101 ADC Enable, ADC Start-Coversion, (No Freerunning),ADIF Clear
	ADCSRA=0xFD;						//			 Interrup Enable,  Prescaler -> 32
}



void portsInit()
{
	//sbi(DDRB,0);			//OUT Summer
	//sbi(DDRB,1);			//OUT LED
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
	
	sei();                  // enable interrupts
	
	UARTInit();
	//DDRC=0xff;
	uint8_t z;
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
//----------------------------------------------------------------------


