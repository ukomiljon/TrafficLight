# TrafficLight
Asp.Net core microservices in Real Time with SignalR and single messages to send to multiple consumers with RabbitMQ message broker.

![Traffic Light System-Page-2 (4)](https://user-images.githubusercontent.com/16934572/121951886-15d0fd00-cd8e-11eb-8b3e-03080f5991f3.png)

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
