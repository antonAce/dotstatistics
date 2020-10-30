# dotstatistics


![Documentation](https://img.shields.io/github/workflow/status/antonAce/regression-net/Backend%20CI?label=Backend%20CI)
![Documentation](https://img.shields.io/github/workflow/status/antonAce/regression-net/Frontend%20CI?label=Frontend%20CI)


### Description


**DotStatistics** is an open online multi-user platform for statistical analysis. Created using **.NET Core** technologies.


### Implemented alogrithms


* [Matrix operations](src/DotStatistics.Numeric/Primitives/Matrix.cs)
  * [Basic arithmetic operations](src/DotStatistics.Numeric/Extensions/MatrixExtensions.cs)
  * [Conversion to built-in types](src/DotStatistics.Numeric/Extensions/ConversionExtensions.cs)
* [Linear Algebra](src/DotStatistics.Numeric/LinearAlgebra)
  * [Gauss-Jordan method](src/DotStatistics.Numeric/LinearAlgebra/GaussJordanRootEliminator.cs)
* [Optimization](src/DotStatistics.Numeric/Optimization)
  * [Least squares linear regression](src/DotStatistics.Numeric/Optimization/LeastSquaresRegressionOptimizer.cs)


### Technologies stack

* [ASP.NET CORE](https://github.com/dotnet/aspnetcore)
* [ANGULAR](https://github.com/angular/angular)
