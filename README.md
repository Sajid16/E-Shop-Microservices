# E-Shop-Microservices
Building microservices for online shopping with vertical slice alrchitecture, Clean architecture, Event driven communication and many more....

# Docker
- docker-compose.yml:
    * This is the primary configuration file for defining your Docker services and settings.
    * It contains the base configuration for your Docker containers, including which images to use, the configuration for networking, volumes, environment variables, and any other 
    Docker settings.
    * This file is typically version-controlled and contains the core definitions that are common across environments (e.g., production, development).
    * All the essential settings that define how the Docker containers should behave are placed in this file.
- docker-compose.override.yml:
    * This file is used to override or extend the configurations in docker-compose.yml, typically for development or local settings.
    * By default, Docker Compose automatically looks for this file alongside docker-compose.yml, and it will merge the contents of the two files.
    * This file is useful when you want to customize the configuration for specific environments (like development), without changing the main docker-compose.yml.
    * It is optional and is usually not version-controlled (or versioned differently) because it contains configurations that are environment-specific or local to the developer's 
      machine (like different ports, volumes, environment variables, etc.).
