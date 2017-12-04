const int trigPin = 4;
const int echoPin = 5;
int IRpin = 1;
long duration;
float distance;
float volts;
int r;
int i;
void setup() {
pinMode(IRpin,INPUT);  
pinMode(trigPin, OUTPUT); // Sets the trigPin as an Output
pinMode(echoPin, INPUT); // Sets the echoPin as an Input
}
float f(int trigPin1,int echoPin1)
{
  r=0;
  for(i=1;i<=2;i++)
  {
    digitalWrite(trigPin1, LOW);
    delayMicroseconds(2);
    digitalWrite(trigPin1, HIGH);
    delayMicroseconds(10);
    digitalWrite(trigPin1, LOW);
    duration = pulseIn(echoPin1, HIGH);
    r+= duration*0.034/2;
  }  
  return r/2;
}
void MakeTone(float n)
{
  int noteDuration = 1100/4;
    tone(3,n,noteDuration);
    noTone(8);
}

void loop() {
  distance=f(trigPin,echoPin);
  if(distance<15)
  {
    MakeTone(100);
  }
  if(distance>30)
  {
    
   volts = analogRead(IRpin)*0.0048828125;
   distance = 65*pow(volts, -1.10);        
   //here you should map distance to tone
   //y = map(distance, 30, 150, 350, 100);  
   //MakeTone(y)
   Serial.println(distance);  
  }
  
}
