# practical-clean-ddd

The practical repository uses coolstore domain which is mainly borrowed from `https://github.com/zkavtaskin/Domain-Driven-Design-Example` to demonstrate how to apply Domain Driven Design seamlessly with Clean Architecture.

# Business usecases

![](assets/usecase_diagram.png)

# High level context

![](assets/context_diagram.png)

# ERD

![](assets/class_diagram.png)

# Testing Application

Starting the web application

```
$ cd src\Web
$ npm i
$ npm run dev
```

Starting the Api

```
$ tye run
```

```
$ cd src\Product\ProductService.Api\
$ dotnet run
```

> Frontend: [http://localhost:3000/products](http://localhost:3000/products)
> 
> Backend: [http://localhost:5002](http://localhost:5002)
> 
> Tye Dashboard: [http://localhost:8000](http://localhost:8000)

![](assets/products_screen.png)

# Refs
- [C4 PlaintUML Model](https://github.com/plantuml-stdlib/C4-PlantUML/blob/master/samples/C4CoreDiagrams.md)
- [Real world PlantUML](https://real-world-plantuml.com)