# promotion_engine
Some practice Application

This is a project developed to design the promotion engine. 

Project components - 
Promotion.Engine.API - API Project developed using DDD concepts and as a microservice which can be hooked to any checkout process and configured to be reused. 

Promotion.Engine.Domain - Domain (class library) developed for domain aggregates defitions and declaration of interfaces for repositories as well as seed data. 

Promotion.Engine.Tests 

Unit test project developed using NUnit and only test for command handler has been invoked. 

The ApplyPromotionCommandHandler can only be invoked from Unit test perspective as the idea is to follow Test Driven Development. 

Idea was, first the test method for the handler should pass for the given test cases and then only the repositories implementation, controller implementations was supposed to be implemented. 

Currently the unit test is passing as per the given test cases for apply promotions. 
Going forward in further expansion addition of new promotions and it's test cases and unit test method supposed to be added. 
