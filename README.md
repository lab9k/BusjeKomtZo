# Busje komt zo
Web interface for tracking shuttle buses in Ghent

## Requirements

 - Dotnet core
 - Google Chrome version 60 or higher
 - Chrome Driver

## Description
This application fetches data from the online tracking map, visualizes and processes it to show helpful information to users waiting for a shuttle.

Currently the application initiates a headless Chrome session and fetches a session id from the remote tracking site, that id is then used to connect to a remote API to fetch useful data.

## TODO

 - Replace SessionFetcher with a proper API implementation
 - Calculate how long it will take till the next bus arrives
