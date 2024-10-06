data=readtable("MyTest243025.csv");
%folgenden Kram hab ich nur gemacht um grooob rauszufinden, ab welchem
%Punkt sich überhaupt samples ändern um damit die überflüssigen Werte
%rauszufilter, hat nicht so gut geklappt.....
%v=[];
% for k=1:10000
% if data.Var1(k)~=data.Var1(k+1)
%     v=[v k]
% end
% end

%... deshalb nehme ich einfach mal jeden 100. Wert. Der Plot sieht optisch
%damit sehr ähnlich aus.
datadown=data.Var1(1:100:length(data.Var1)); %Picke jeden 100. Wert des Signals

dataac=datadown-mean(datadown); %Entferne den Gleichanteil

%Annahme: Zwischen 2 maxima liegen uuungefähr 600ms; entspricht einem
%100er Ruhepuls
%Beobachtung: Zwischen 2 Maxima liegen uuungefähr 900 Samples
% Damit ergibt sich grob ne Abtastperiode von 600ms/900 Samples -> Ta=2/3ms
% ; fa = 1,5kHz

%erzeuge Zeitbasis:
Ta=2/3;
t=(1:length(dataac))*Ta; %t in ms
figure(1)
plot(t,dataac') %Plot des Signals mit Zeitbasis
xlabel("Zeit in ms"), ylabel("Amplitude")

%Darstellung des Spektrums:
spec=fft(dataac,2^floor(log2(length(dataac)))); %Berechnung der gesamten FFT
f=(1:length(spec))/Ta/length(spec)*1000; %Frequenzbasis in Hz
figure(2)
plot(f(1:length(f)/2),abs(spec(1:length(f)/2)))
xlabel("Frequenz in Hz"), ylabel("Amplitude")

