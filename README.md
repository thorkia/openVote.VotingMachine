# openVote.VotingMachine

This is an expirement to develop a Voting Station/Polling booth in an open manner.

This project will use the following technologies:

### Hardware:

* Raspberry PI 2 with power supply
* Official Raspberry PI Touch Screen
* SD Card for storage
* Power cable to power the PI from the screen

### Software:

* Windows IoT
* Windows UWP
* Visual Studio 2015
* SQLite for local storage


### Roadmap:

* Create HTTP server to run on Control Machine
  * Record all votes
  * Set unlock command
  * Support logging
* Add client request unlock from server
* Add client request to store vote on server
* Add in-depth logging to client
  * Logs should be stored locally and on server
