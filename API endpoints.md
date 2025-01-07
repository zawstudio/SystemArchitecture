# Media API Documentation

## Get All Media Items
**Endpoint**:  
`GET /media`

**Description**:  
Fetches all media items from the system.

**Headers**:
- `Authorization`: Bearer [token] (required if the endpoint is protected)

**Response**:
- **Status: 200 OK**  
  Returns a list of all media items.
```json
[
  {
    "id": 1,
    "name": "Sample Book",
    "author": "Author Name",
    "bookCode": "CODE123",
    "issueDate": null,
    "returnDate": null,
    "genre": "Fiction",
    "isBorrowed": false,
    "description": "Description here",
    "imageUrl": "https://example.com/image.jpg"
  }
]
```

- **Status: 400 Bad Request:**  
  Returns an error message if an exception occurs.
```json
{
  "error": "An error occurred."
}
```

## Get Media Item by ID
**Endpoint**:
`GET /media/{id}`

**Description**:  
Fetches a media item by its unique ID.

**Path Parameters:**
- `id` (required): The ID of the media item.

**Response**:

- **Status: 200 OK**  
  Returns the media item with the specified ID.
```json
{
  "id": 1,
  "name": "Sample Book",
  "author": "Author Name",
  "bookCode": "CODE123",
  "issueDate": null,
  "returnDate": null,
  "genre": "Fiction",
  "isBorrowed": false,
  "description": "Description here",
  "imageUrl": "https://example.com/image.jpg"
}
```

- **Status: 404 Not Found**  
  Returns an error message if the media item with the specified ID is not found.
```json
{
  "error": "Media item not found."
}
```

## Update Media item
**Endpoint**:
`PUT /media/{id}`

**Description**:
Updates the details of a media item.

**Path Parameters:**
- `id` (required): The ID of the media item.

**Request Body**:
```json
{
"id": 1,
"name": "Updated Book Name",
"author": "Updated Author",
"bookCode": "UPDATEDCODE",
"genre": "Updated Genre",
"description": "Updated description",
"imageUrl": "https://example.com/new-image.jpg"
}
```

**Response**:
- 204 No Content:
  The Media item was updated successfully.
- 400 Bad Request:
  An error occurred while updating the media item.

## Search Media
**Endpoint**:
`GET /media/search`

**Description**:
Searches for media items using a query string.

**Query Parameters**:
- `query` (string, required): The search query.
- `page` (int, optional): Page number
- `pageSize` (int, optional): Items per page

**Response**:
- **Status: 200 OK**  
  Returns a matching media items.

- **400 Bad Request:**  
  If the search query is invalid.

## Get Borrowed Media

**Endpoint**:
`GET /media/borrowed`

**Description**:
Fetches all borrowed media items.

**Response**:
- **Status: 200 OK**  
  Returns a list of borrowed items.

- **Status: 400 Bad Request:**  
  Returns an error message if an exception occurs.

## Borrow Media Item
**Endpoint**:
`POST /media/{id}/borrow`

**Description**:
Borrows a media item by its ID.

**Path Parameters**:
- `id` (required): The ID of the media item to borrow.

**Response**
- **Status: 200 OK**  
  Confirms the media item was borrowed.
```json
{
  "message": "Media item borrowed successfully."
}
```
- **Status: 400 Bad Request**  
  The item is already borrowed.

## Return Media Item

**Endpoint**:
`POST /media/{id}/return`

**Description**:
Returns a borrowed media item by its ID.

**Path Parameters**:
- `id` (required): The ID of the media item to return.

**Response**:

- **Status: 200 OK**  
  Confirms the media item was returned.
```json
{
  "message": "Media item returned successfully."
}
```

- **Status: 400 Bad Request**  
  The item is not borrowed.
```