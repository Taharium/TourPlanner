<center><big>Protocol</big></center>

- Screenshot of User Interface 
    <img title="User Interface" alt="Screenshot of User Interface" src="UI_image.png">

- Description of UI
    ```
    On top of the Interface we have a search bar where the user can search for tours that are similar or for a specific tour. Beneath that on the left side, we have the tour list. Here, the user can add, modify or delete a tour. For adding, the user should klick the "add tour" button. For the other operations, there are buttons foreach tour in the list. A new window pops up if the user decides to either add a tour or modify an existent tour. On the right side of the page, there are three tabs. In the general tab, there is the tour information (Date, Start Location, End Location, ...). In the route tab, there will be a map of the tour. Beneath those tabs, there are the tour logs for a specific tour. Here you can also add, modify or delete a tour log. In the same way, new windows will pop up for adding or modifying a tour. 
    ```
    

- Wiremock for Add tour operation 
<br/>
    <img title="Add Tour" alt="Wireframe of Add Tour Function" src="Wiremock_Add-Tour.drawio.png" width="500" height="600">


- Description of app architecture
    ```
    We decided to layer this project in to three layers. The Presentation Layer is responsible for handling everything that happens immediately with the user. It should give feedback to the user so that he known at any time in which state he is currently on. Furhermore, this layer calls on layer below to the businesslayer for various tasks. Here, we have our connection to the OpenRouteService API and the computing of our Popularity and Child friendliness (Computed Attributes). Furthermore, for the CRUD operations that the user wants to fulfill for the Tours and TourLogs, the Businesslogic calls Services that are still in the Businesslayer. Every operations has its own service. Before we call a UnitofWork in the DAL layer, we convert our Model from the Frontend to a DTO that is saved in the Database. We do that because we have code in our Tour and TourLog Models that are just for the frontend. As said before we now call a UnitofWork and that gives us access to the repository. And in there, we now can execute the CRUD operation, that was requested from the user, on the database. 
    ```

    - Class Diagram BL
<br/>
        <img title="Class diagram BL" src="ClassDiagramBL.png">
<br/>
    - Class Diagram PL
<br/>
        <img title="Class diagram PL" src="ClassDiagramPL.png">
    - Class Diagram DAL
<br/>
        <img title="Class diagram DAL" src="ClassDiagramDAL.png">
    - Class Diagram DTOandModels
<br/>
        <img title="Class Diagram DTOandModels" src="ClassDiagramDTOandModels.png">


<br/>

- Description of Use Cases
    ```
    If we break it down, the user can activley do most of the CRUD operations. He can Add, Edit and Delete a tour as well as Add, Edit and Delete a TourLog. But in the background they are not all just happening if the user wants to do them. For example, at any operations with the Tourlogs(ADD, EDIT, DELETE) the Edit Tour case has to be done too because it effects the content of the tour itself.   
    ```

    - Use Case Diagram(not all use cases depicted)
<br/>
        <img title="Use Case Diagram" src="UseCaseDiagram.png">

    - SequenceDiagram(GetTours)
<br/>
        <img title="Sequence Diagram GetTours" src="GetToursSequenceDiagram.png">

- Library Decicions / Lessons learned
    ```
    We are using the libraries that we talked about in the course. The only one we use that we didnt talk about is for the generic host. We use this because we had a lot of problems with Dependency Injection in general and because of that we talked about it with other groups and most of them have used the generic host. Before that, we had the IoC Container but in order to get the best help from colleagues, we thought it would be the best idea to make it that way. 
    What we have learned is that, DI can help you with a lot of things but can also make your program unusable. For example, we had a lot of trouble injecting our database into our project. Because the AddDbContext function makes it a scoped service, it cant be hold by a singelton and that ruined our hole application. Then we made a factory for the DBContext but now we had the problem that the Respository and our UnitofWork didnt use the same Context and no operations could be fulfilled. At the end we used the DBContextfactory from the Hostbuilder service and a factory for the unitofwork. The only "problem" is that the UnitofWork Factory is directly dependent on the UnitofWork class.
    ```

- Implemented Design Patterns
    ```
    As mentioned before, we implemented the factory pattern. One time for UnitofWork and a second time for our Logging. Moreover, we implemented the MVVM pattern for our frontend. 
    ```

- Unit Tests decisions
    ```
    ```

- Tracked time
    ```
    In total, we came up to about 65 hours each.  
    ```

- Link to GIT
    - https://github.com/if22b151/Tour_Planner