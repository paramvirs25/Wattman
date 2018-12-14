# Angular 

Steps to add a new Module:
1. Create new component -
- open path in explorer and run in cmd.exe mode . Add component by "ng generate component nameofComponent" ;
- exclude its unwanted files from project.
- work on html page.

- add component path to "app.routing.ts".

2. create models in "_models" folder and add its references in respective "index.ts" file. Made models structure same as that of webApi Models.

3. create service in "_services" folder and add its references in respective "index.ts" file and then in "app.module.ts" file .
 - Create methods to get/add/edit/delete data and add routes based on respective controller class from webApi Controller.

