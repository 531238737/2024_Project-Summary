clear;
close all
clc

format long

fc=5000000;%发射频率

Samp=0.00000002;%采样时间
fs=1/Samp;%采样频率
t=-(100/fc):Samp:100/fc;%采样时间


L=length(t);
n=0:L-1;
%N=166700;
N=length(t)*100;
k=0:N-1;

Np=2.5;%
%y=1*(heaviside(t)-heaviside(t-5/fc)).*(1-cos(2.*pi.*fc.*t./5)).*exp(i*2.*pi.*fc.*t);
y=1/4*(heaviside(t)-heaviside(t-Np/fc)).*(1-cos(2.*pi.*fc.*t./Np)).*sin(2.*pi.*fc.*t);


X1=fft(y,N)/fs;
absX1=abs(X1);
KK=max(absX1);

w=2*pi*k/N;
F=(w/(2*pi))*fs;%获得频率轴

%[f,mx]=fft_origin(fs,y);
t=t*10^9;
figure 
plot(t,y,'linewidth',1.5);
%title('Input signal')
xlabel('Time t (\mus)','FontSize',14)
ylabel('Voltage (V)','FontSize',14)
% xlim([0,60])
X2=20*log(abs(X1)/KK);
%X2=20*log((X1)/KK);

[CC,II]=max(real(X2));


%[I11,I12]=find(real(X2)>0.707*CC*0.99 & X2<0.707*CC*1.01);
%[I11,I12]=find((X2)<-3*0.99 & X2>-3*1.01);
%Bdth=2*abs(II-I12(1))*(F(2)-F(1))


% Fbx=fc-1/2*Bdth:Bdth/200:fc+1/2*Bdth;
% %Fby=20*log(F(I12(2)))*ones(1,length(Fbx));
% Fby=20*log(F(II)*ones(1,length(Fbx)));

figure 
%plot(F/1000,abs(X1));
plot(F/1000000,absX1*fs,'linewidth',1.5);hold on; 
%plot(fc-1/2*Bdth,F(I12(2)),'r*');hold on; 
%plot(fc+1/2*Bdth,F(I12(2)),'ro');hold on; 
% plot(Fbx,Fby,'ro');
%title('Input signal')
axis([0,200,0,max(absX1)*fs]);
xlabel('Frequency (MHz)','FontSize',14)
ylabel('Magnitude','FontSize',14)
%ylabel('Magnitude in Frequency spectrum (dB) ')

