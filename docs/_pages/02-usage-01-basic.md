---
layout: default
title: Usage Guide
permalink: /usage/index.html
---

## Basic usage

### `As<T>` extension method

For basic use of Ditto, let's start with a standard DocumentType with a few properties:

* **Title** (alias = "title") - using a _Textstring_ property
* **Body Text** (alias = "bodyText") - using a _Richtext editor_ property
* **Image** (alias = "image") - using an _Upload_ property

![Umbraco DocType properties](/assets/img/umbraco-doctype-properties.png)

Using those properties as the structure of our view-model (POCO class), we can manually create a C# class:

```csharp
public class MyTextModel
{
	public string Title { get; set; }
	
	public string BodyText { get; set; }
	
	public string Image { get; set; }
}
```

> Note: in this case **the property names should correspond with the DocumentType's property aliases.**<br>
> _If you wish to name your properties differently, please [see the documentation about the `UmbracoProperty` attribute.](/usage/processors/#umbracoproperty)_
> 
> The C# class name (`MyTextModel` in this example), **does not** need to match with the DocumentType alias.

Now you can map your `IPublishedContent` (content node of that DocumentType) to your corresponding POCO model using the `As<T>` extension method.

```csharp
var poco = Model.Content.As<MyTextModel>();
```

Here is an example in the context of an MVC view:

```html
@using Our.Umbraco.Ditto
@inherits UmbracoTemplatePage
@{
	var poco = Model.Content.As<MyTextModel>();
}

<h1>@poco.Title</h1>

@poco.BodyText

<img src="@poco.Image" alt="..." />

```

For further examples, please read the [advanced topics](/advanced/).

#### Video demonstrations

* A short screencast giving [a brief introduction to Ditto](http://www.screenr.com/3oRN) (5 mins)
* Lee Kelleher showcases Ditto on [uHangout EP40](https://www.youtube.com/watch?v=L40haIBLNS4)
