# Traffic Light Sytem
Real time Asp.Net core microservices with SignalR and single messages to send to multiple consumers with RabbitMQ message broker.

### Definition:

It is required to implement a traffic light system with 4 sets of lights, as follows. <br /><br />
Lights 1: Traffic is travelling south <br />
Lights 2: Traffic is travelling west <br />
Lights 3: Traffic is travelling north<br />
Lights 4: Traffic is travelling east<br /><br />
The lights in which traffic is travelling on the same axis can be green at the same time. During normal hours all lights stay green for 20 seconds, but during peak times north and south lights are green for 40 seconds while west and east are green for 10 seconds. Peak hours are 0800 to 1000 and 1700 to 1900. Yellow lights are shown for 5 seconds before red lights are shown. Red lights stay on until the cross-traffic is red for at least 4 seconds, once a red light goes off then the green is shown for the required time(eg the sequence is reset). <br />

Advanced: At this intersection north bound traffic has a green right-turn signal, which stops the south bound traffic and allows north bound traffic to turn right. This is green at the end of north/south green light and stays green for 10 seconds. During this time north bound is green, north right-turn is green and all other lights are red. 

### Implementation/Outcomes:

1.	Implement a front-end and backend 
2.	The backend will contain the logic and state of the running traffic lights. The front-end will be a visual representation of the traffic lights, with the data served from the backend. 
3.	There’s no need to have a perfect design on the front end, something simple and functional is fine. Noting* we will review the client side code.
4.	There’s no need to implement entity framework (or similar) to store the data in a database, a in-memory store is fine
5.	Code needs to follow architecture & best practices for enterprise grade systems

## Solution
It uses this sequence of traffic lights. https://www.youtube.com/watch?v=eZ33_lEjgxo

For example:
      South/North: G, Y, R, R, R, R
      West/East:   R, R, R, G, Y, R
   
But in order simplify initial value, all traffic lights are initially in red signal.
Then the sequences look like this

      South/North: R, G, Y, R, R, R
      West/East:   R, R, R, R, G, Y

### Brief documentation:
 
1. Traffic Light Central System microservice - It can be managed by traffic admin, to set initial states, signal stay time for normal, and pick hours. And it sends a message to Message Broker (RabbitMQ) as a producer message.
2. East, West, North, South traffic lights microservices. They are consumer messages from the Traffic Light Central System to receive the state of signal and send about its signal to a client (user view). It is used SignalR to send about the state to clients.
3. Console App as a receiver state of signal from East, West, North, South traffic lights microservices and displays results to clients.
4. There are EventBus.Messages as a message type to understand microservices when send/receive a message (about state signal) and SignalRHub is similar RPC for  East, West, North, South traffic lights microservices to send message to the client using SignalR.
5. There is QueueBuilderV1 in Traffic Light Central System microservice use-case/Rules to build sequences of signals based on https://www.youtube.com/watch?v=eZ33_lEjgxo. However, the sequences can be a varied version of builders based on required statements. 
 
### Easy steps.

Note: it is pre required 
- .net core framework
- docker desktop
- visual studio 2019.

1. git clone 
2. open in visual studio run  ```docker-compose up -d``` 
3. run without debug on visual studio. you can see console applications for microservices and client app, and swagger apis on browser
   1. Traffic Light Central System swagger api. http://localhost:5000/swagger/index.html
   2. East Traffic Light api.  http://localhost:5001/swagger/index.html
   3. West Traffic Light api. http://localhost:5002/swagger/index.html
   4. North Traffic Light api. http://localhost:5009/swagger/index.html
   5. South Traffic Light api. http://localhost:5004/swagger/index.html
4. You can see on Client Consol App where ready to recieve message from 4 microservices and all microservices.
   
   ![image](https://user-images.githubusercontent.com/16934572/122234747-b4c13a80-ceef-11eb-955e-9b5f146cd0cb.png)

5. Copy from bellow of this readme an example of json payload of traffic lights settings and paste and post it on Traffic Ligth Central System swagger api

![image](https://user-images.githubusercontent.com/16934572/122228697-afadbc80-ceea-11eb-8ade-1120ee054332.png)

6. After creating initial settings of traffic lights execute run commands using Traffic Ligth Central System swagger api

![image](https://user-images.githubusercontent.com/16934572/122229109-0ca97280-ceeb-11eb-8af0-9b77c6f12473.png)

7. You can see all microservices east, west, north, south start to recieve state of signals from traffic light central system api, and send each its own signal to the cliens app console. Traffic Light Central System Console displays state of signals where generates and sends to west, east, north and south traffic light microservices.

 ![image](https://user-images.githubusercontent.com/16934572/122235359-3749fa00-cef0-11eb-98d1-75e371336ab2.png)
 
8. it can be stoped by execute stop commands in Traffic Light Central System swagger api

![image](https://user-images.githubusercontent.com/16934572/122230716-79713c80-ceec-11eb-8e6d-6b1eb42aefdd.png)

9. it can be created other intersections traffic lights to create and manage (run, stop) with  Traffic Light Central System swagger api


### Architecture
![Traffic Light System-Page-2 (4)](https://user-images.githubusercontent.com/16934572/121951886-15d0fd00-cd8e-11eb-8b3e-03080f5991f3.png)

 

### Traffic Light Central System to manage whole traffic light and other microservices signal state.
![image](https://user-images.githubusercontent.com/16934572/121990646-ae886c80-cdd0-11eb-9d0f-b52b570039b5.png)


### Example of json payload for initial traffic light bounds
```
{
  "intersectionName": "abc",
  "trafficBounds":[
  {
    "pickHours": [
      {
        "red": 4,
        "yellow": 5,
        "green": 10,
        "rightTurnGreen": 0,
        "start": "17:00",
        "end": "19:00"
      },
     {
        "red": 4,
        "yellow": 5,
        "green": 10,
        "rightTurnGreen": 0,
        "start": "08:00",
        "end": "10:00"
      }
    ],
    "normalHour": {
      "red": 4,
      "yellow": 5,
      "green": 20,
      "rightTurnGreen": 0
    },
    "trafficLightBound": "West" 
     
  },
 {
    "pickHours": [
      {
        "red": 4,
        "yellow": 5,
        "green": 10,
        "rightTurnGreen": 0,
        "start": "17:00",
        "end": "19:00"
      },
     {
        "red": 4,
        "yellow": 5,
        "green": 10,
        "rightTurnGreen": 0,
        "start": "08:00",
        "end": "10:00"
      }
    ],
    "normalHour": {
      "red": 4,
      "yellow": 5,
      "green": 20,
      "rightTurnGreen": 0
    },
    "trafficLightBound": "East" 
     
  },
{
    "pickHours": [
      {
        "red": 4,
        "yellow": 5,
        "green": 40,
        "rightTurnGreen": 0,
        "start": "17:00",
        "end": "19:00"
      },
     {
        "red": 4,
        "yellow": 5,
        "green": 40,
        "rightTurnGreen": 0,
        "start": "08:00",
        "end": "10:00"
      }
    ],
    "normalHour": {
      "red": 4,
      "yellow": 5,
      "green": 20,
      "rightTurnGreen": 0
    },
    "trafficLightBound": "South" 
     
  },
{
    "pickHours": [
      {
        "red": 4,
        "yellow": 5,
        "green": 40,
        "rightTurnGreen": 0,
        "start": "17:00",
        "end": "19:00"
      },
     {
        "red": 4,
        "yellow": 5,
        "green": 40,
        "rightTurnGreen": 30,
        "start": "08:00",
        "end": "10:00"
      }
    ],
    "normalHour": {
      "red": 4,
      "yellow": 5,
      "green": 20,
      "rightTurnGreen": 10
    },
    "trafficLightBound": "North" 
    
  }
]
}
```
