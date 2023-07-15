## Brewery SCADA

The Brewery SCADA Application is a powerful simulation of a Supervisory Control and Data Acquisition (SCADA) system tailored specifically for breweries. Designed to optimize brewery operations, this application enables you to track real-time values from your SCADA system, add custom tags, set up alarms, receive timely notifications, and generate comprehensive reports.

With the Brewery SCADA Application, you gain valuable insights into your brewery processes, ensuring efficient monitoring, control, and analysis. Whether you're managing temperature, pressure, flow rates, or other critical parameters, this application provides a centralized platform for real-time value tracking. Stay informed about the current state of your brewery operations and make informed decisions to maintain the highest standards of quality and efficiency.


## Backend
The backend of the Brewery SCADA Application is developed in C# using the .NET framework. It leverages SignalR for real-time communication and utilizes the SQLite database for fast development.

The backend enables real-time communication with clients through SignalR, allowing for seamless transmission of real-time values from the SCADA system. It supports two types of users: admins and workers. Admins have privileged access for managing user accounts, system settings, and overseeing the application's functionality. Workers have limited access based on their roles within the brewery.

To expedite development, the backend integrates the lightweight and efficient SQLite database. This choice facilitates rapid prototyping and simplifies data storage and retrieval.
## Web Frontend
The frontend of the Brewery SCADA Application is built using React with TypeScript, providing a powerful and flexible framework for developing dynamic user interfaces. It follows a modern and minimalistic design approach, offering a clean and intuitive user experience.

The frontend design focuses on simplicity and ease of use, ensuring a smooth and engaging user experience. It utilizes Material UI, a popular React UI framework, for enhanced visual appeal and consistent design patterns.

To provide timely notifications, the frontend integrates the Hot Toast library, simplifying the implementation of customizable notifications.

## Features
### Database Manager
The DB Manager, an exclusive feature of the Brewery SCADA Application, empowers administrators with robust control and management capabilities over the application's database. Designed exclusively for admins, this powerful tool offers unparalleled functionality to streamline operations and ensure efficient monitoring.

The DB Manager also allows admins to manage tags within the SCADA system. Admins can delete unnecessary tags, ensuring a streamlined and organized tag structure. This ensures that the system focuses only on the relevant tags for accurate monitoring and analysis.

Furthermore, admins can exercise control over the scanning of device values by tag. This capability enables admins to selectively turn on or off the scanning of specific tags, optimizing resource utilization and reducing unnecessary data processing.
  ![image](https://github.com/jokicjovan/Brewery-SCADA-System/assets/51921035/5d5c0649-a07a-47d2-ba75-5d38576fdb58)

  In the Brewery SCADA Application, admins have the ability to add new tags to the SCADA system. This functionality allows admins to expand and customize the monitoring capabilities of the application based on the specific needs of their brewery.

By adding new tags, admins can track additional data points relevant to their brewery's processes. Whether it's monitoring specific equipment, capturing specialized parameters, or tracking unique variables

  ![image](https://github.com/jokicjovan/Brewery-SCADA-System/assets/51921035/c685a31f-711f-46fc-8889-ac219f96de03)

  Moreover, the DB Manager allows you to add new alarms or delete existing ones. Alarms are essential for swiftly detecting abnormal conditions or breaches of predefined thresholds. With full control over alarm configuration, you can fine-tune the system to your specific requirements. Receive timely notifications and respond promptly to potential issues, ensuring the smooth operation of your brewery.

  ![image](https://github.com/jokicjovan/Brewery-SCADA-System/assets/51921035/18b6178f-7a9e-41b0-b6d6-1391dec4f16b)

  ![image](https://github.com/jokicjovan/Brewery-SCADA-System/assets/51921035/c163faa3-b53a-4fa3-b7e9-4bdc1d5ac1f7)

### Trending
The trending feature of the Brewery SCADA Application allows admins and workers to efficiently monitor multiple tags within the SCADA system. This feature provides real-time visibility into tag values, alarms, alerts, and essential tag information.

Users can monitor the real-time values of multiple tags simultaneously, including critical parameters such as temperature, pressure, and flow rates. This enables admins and workers to stay informed about the current state of brewery operations and make data-driven decisions.

The trading feature also tracks alarms associated with specific tags, ensuring potential issues are promptly identified. Users can receive alert notifications, allowing for proactive response and minimizing disruptions.

Additionally, users can access basic information about each tag, including names, descriptions, units of measurement, and relevant metadata. This facilitates a better understanding of the monitored parameters and enhances data interpretation.

![image](https://github.com/jokicjovan/Brewery-SCADA-System/assets/51921035/c120e1e4-1f98-4142-94d5-1ab4dd869810)

### Simulation
During the development phase, as access to real devices that send values to the database is limited, the Brewery SCADA Application utilizes a background process in .NET to simulate the data transmission process. This simulation replicates the behavior of real devices, providing a reliable testing environment.

To configure the simulation parameters, the application employs a SCADA config JSON file. This file allows users to define various simulation settings to accurately mimic the behavior of the desired devices.

In the case of RTU simulation, the background process generates a random number within the specified low and high levels, as defined in the SCADA config file. This random number generation closely resembles the variability observed in real-time data from devices connected via RTU (Remote Terminal Unit).

The frequency of sending data is another adjustable parameter within the SCADA config file. It determines how often the simulated values are sent, ensuring a realistic data transmission pattern.

Alternatively, for tags that do not utilize RTU simulation, the application provides a simple simulation option. This simulation enables the generation of values based on mathematical functions such as sinus, cosinus, and ramp functions. The specific function to be used is determined by a parameter in the SCADA config file, allowing for customization based on individual tag requirements.

### Reports
The Brewery SCADA Application's reports feature offers admins a powerful tool for in-depth data analysis. With various report types, admins can gain valuable insights into the brewery's operations and make informed decisions.

Admins can generate reports that include all alarms within a specified date range. By sorting alarms by priority and time, admins can identify critical events and patterns, enabling them to prioritize and address issues effectively.

Furthermore, admins have the ability to generate filtered reports specifically for alarms with defined priorities. This allows them to focus on critical alarms and take appropriate action promptly.

The reports feature also provides admins with comprehensive tag value reports within specific time periods. This enables admins to analyze performance, identify trends, and make data-driven decisions based on detailed data analysis.

Additionally, admins can generate reports that provide the latest values of analog and digital tags. This feature allows for a quick snapshot of real-time data, facilitating monitoring and assessment of key parameters.

Admins also have the flexibility to generate reports specific to a particular tag by using its unique tag ID. This allows for detailed analysis and troubleshooting, providing insights into historical data and aiding in pattern identification.
![image](https://github.com/jokicjovan/Brewery-SCADA-System/assets/51921035/17046083-a4c5-4344-a00a-baae54893fcd)

