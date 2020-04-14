# Regression.NET

![Documentation](https://img.shields.io/github/languages/code-size/antonAce/Regression.NET)
![Documentation](https://img.shields.io/github/languages/count/antonAce/Regression.NET)

ASP.NET Core + Angular application for processing datasets using regression modeling methods.

## Description

Regression processing algorithms:

- Least squares method;

Supported regression models:

- Multiargument linear;

Dataset storing providers:

- File system;

## Technologies stack

- ASP.NET Core 3.1;
- Angular 8;
- Bootstrap 4.4;
- NG KaTeX 2;
- RXJS 6;
- D3JS;

## API Map

### Dataset storage

Datasets listing

```http
GET api/dataset?limit=(:int:)&offset=(:int:)&headersOnly=(:bool:)
```

Get dataset by GUID

```http
GET api/dataset/(:GUID:)?outputsOnly=(:bool:)
```

Store new dataset

```http
POST api/dataset
Content-Type application/json

{
    "Name": "(:string:)",
    "Records": [{
        "Inputs": [(:double:), ...],
        "Output": (:double:)
    },
    ...]
}
```

Update dataset by GUID

```http
PUT api/dataset/(:GUID:)
Content-Type application/json

{
    "Name": "(:string:)",
    "Records": [{
        "Inputs": [(:double:), ...],
        "Output": (:double:)
    },
    ...]
}
```

Drop dataset by ID

```http
DELETE api/dataset/(:GUID:)
```

### Data processing

Regression processing

```http
POST api/processing?digits=(:int:)
Content-Type application/json

{
    "Records": [{
        "Inputs": [(:double:), ...],
        "Output": (:double:)
    },
    ...]
}
```

### Uploading CSV

Store dataset from file

```http
POST api/fileUpload
enctype multipart/form-data

name = (:string:)
file = (:IFormFile:)
```
