# WorkPlanningService
//Build a REST application from scratch that could serve as a work planning service.
//The application is part of a microservice setup, so being able to pick and put messages from a queue is necessary. (Just setup the service and interface) 

//Business requirements.
//1. A worker has shifts
//2. A shift has a start and end date
//3. A worker can not work two shifts at the same time
//4. A worker never has two shifts in a row.
//5. It is a 24 hour timetable

2 Microservices: WorkerAPI with methods: create worker, update worker and WorkingPlanningAPI with methods: Add Shift and List Shifts for a worker. 
When a worker name is updated, that the names will be updated also in shifts. APIs are comunicating using RabbitMQ message queue. 
For WorkingPlanningAPI we have set up the env to send and also reveice message.

Using RabbitMQ, CQRS and MediatoR, Swagger.
