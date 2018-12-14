# WebApi

Steps to add a new Module:
1 Create new Model class in 'Models' FOLDER.
	* Create extension model in respective new folder. Following extensions will be created
		a. %ListModel
		b. %CreateGetModel - Typically returns data for UI dropdowns, if needed
			For constants values like dropdown options, create class in FOLDER AppConstants
		c. %EditGetModel - Typically returns data for UI dropdowns and for populating UI form
2 Create a Validator class for Model in 'Validators' FOLDER.
	* Create validation for respective properties of model
3 Create Validation message class and constants to be used in validators in AppConstants FOLDER.
4 Create Service class in Services FOLDER
	* Add service in Startup.cs in dependency injection
5 Create Mappings in AutoMapperProfile in Helpers FOLDER
6 Create Controller class in Controllers FOLDER