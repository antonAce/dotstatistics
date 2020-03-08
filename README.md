# Regression.NET

ASP.NET Core service for building and analyzing regression models

## Description

Supported regression models:
- Multiargument linear;

## Technologies stack

- ASP.NET Core 3.1;
- Angular 8;
- Bootstrap 4.4;
- KaTeX;

# API Map

**POST**
api/processing

HEADERS:
Content-Type application/json

BODY:
{
"Rows": [{
"Id": 1,
"Args": [0.0, 0.0, 0.0],
"Result": 0.0
},
{
"Id": 2,
"Args": [0.0, 0.0, 0.0],
"Result": 0.0
}]
}
