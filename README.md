# TrafficLight
Real time Asp.Net core microservices with SignalR and single messages to send to multiple consumers with RabbitMQ message broker.

TrafficLighSystem
It uses this sequence of traffic lights. https://www.youtube.com/watch?v=eZ33_lEjgxo

For example: South/North: G, Y, R, R, R, R West/East: R, R, R, G, Y, R

The first 2 sequences might be 1 second different, if the unit test fails it needs to run again.

Brief documentation:
TrafficLightServer project - it makes communication with client, it runs with SignalR which can send message to client
TrafficLightService project- core architecture domain knowledge here.
Client Console project - to see results
Common project - it provides DTO, JSON serializer, interfaces for settings and timer where may use across projects
XUniteTestTrafficLightSystem - Unit test for various scenarios with normal / pickhours
Definition:
It is required to implement a traffic light system with 4 sets of lights, as follows.

Lights 1: Traffic is travelling south
Lights 2: Traffic is travelling west
Lights 3: Traffic is travelling north
Lights 4: Traffic is travelling east

The lights in which traffic is travelling on the same axis can be green at the same time. During normal hours all lights stay green for 20 seconds, but during peak times north and south lights are green for 40 seconds while west and east are green for 10 seconds. Peak hours are 0800 to 1000 and 1700 to 1900. Yellow lights are shown for 5 seconds before red lights are shown. Red lights stay on until the cross-traffic is red for at least 4 seconds, once a red light goes off then the green is shown for the required time(eg the sequence is reset).

Advanced: At this intersection north bound traffic has a green right-turn signal, which stops the south bound traffic and allows north bound traffic to turn right. This is green at the end of north/south green light and stays green for 10 seconds. During this time north bound is green, north right-turn is green and all other lights are red.

Implementation/Outcomes:
Implement a front-end and backend
The backend will contain the logic and state of the running traffic lights. The front-end will be a visual representation of the traffic lights, with the data served from the backend.
There’s no need to have a perfect design on the front end, something simple and functional is fine. Noting* we will review the client side code.
There’s no need to implement entity framework (or similar) to store the data in a database, a in-memory store is fine
Code needs to follow architecture & best practices for enterprise grade systems

![Traffic Light System-Page-2 (4)](https://user-images.githubusercontent.com/16934572/121951886-15d0fd00-cd8e-11eb-8b3e-03080f5991f3.png)

Result:
![image](https://user-images.githubusercontent.com/16934572/121987787-c14c7280-cdcb-11eb-8024-b692c02cff61.png)


## json payload for initial traffic light bounds
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
