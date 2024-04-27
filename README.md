To execute the program, ensure the database settings in AppSetting are verified. Redis installation is essential for operation. 
<br>
1- The Domain layer defines the models, while the Application layer defines the Dtos.<br> 
2- To enable Redis Caching, a Decorator was created for Blogrepository using the Cache-aside pattern and Scrutor. (Please review the Redis settings in the configuration to launch the program) Other possible methods include using Attributes, employing the pipeline in Mediator, or using Event sources.<br>
3- The tables in SQL are configured using FluentApi in the Infrastructure layer.<br>
4- Rowversion is utilized in table settings for Optimistic Concurrency.<br>
5- Implementation follows a Clean Architecture approach, incorporating UnitOfWork, Repository, and Cache-Aside patterns.<br>
6- Testing includes Unit tests for validations, handlers, and mappings; Integration tests for Repositories (one example); and Function tests for Apis (one example). <br>

