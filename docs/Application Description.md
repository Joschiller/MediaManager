# Media Manager

<div style="background: #141414;" width="100%" height="640px">
  <img style="margin: 40%; padding-right: 64px" src="./styles/app_icon/icon.png" height="128px"/>
</div>

<div style="page-break-after: always;"></div>

- [1 Introduction](#1-introduction)
- [2 Use Cases](#2-use-cases)
  - [2.1 Using Inputs](#21-using-inputs)
  - [2.2 Editing Catalogs](#22-editing-catalogs)
  - [2.3 Selecting Catalogs](#23-selecting-catalogs)
  - [2.4 Editing Media](#24-editing-media)
  - [2.5 Editing Media Parts](#25-editing-media-parts)
  - [2.6 Editing Tags](#26-editing-tags)
  - [2.7 Searching Media](#27-searching-media)
  - [2.8 Changing Settings](#28-changing-settings)
  - [2.9 Analyzing Data](#29-analyzing-data)
  - [2.10 Editing Playlists](#210-editing-playlists)
  - [2.11 Viewing the Title of the Day](#211-viewing-the-title-of-the-day)
  - [2.12 Viewing Statistics](#212-viewing-statistics)
  - [2.13 Opening the Help Menu](#213-opening-the-help-menu)
- [3 Application Screens](#3-application-screens)
  - [3.x ...](#3x-)
- [4 Stored Data](#4-stored-data)
- [5 Definitions](#5-definitions)
  - [5.1 Types](#51-types)
    - [5.1.1 Catalogue Attributes](#511-catalogue-attributes)
    - [5.1.2 Media Attributes](#512-media-attributes)
    - [5.1.3 Media Part Attributes](#513-media-part-attributes)
    - [5.1.4 Tag](#514-tag)
  - [5.2 Algorithms](#52-algorithms)
    - [5.2.1 Search Parameters](#521-search-parameters)
- [6 Test Cases](#6-test-cases)
  - [6.x ...](#6x-)
- [7 Appendix](#7-appendix)

## 1 Introduction

The _Media Manager_ can be used to sort different media by their attributes. The application supports the definition of different categories for the media and offers a searching functionality as well as several other support features that enhance the creation of media catalogues within the application.

## 2 Use Cases

### 2.1 Using Inputs

|                         **2.1.1** | **Using a tag input**                                                                                                                                                    |
| --------------------------------: | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** |                                                                                                                                                                          |
|                      **Actions:** | 1. Left-click or right-click the checkbox                                                                                                                                |
|                       **Result:** | Left-click: the tag changes it's value from `neutral` to `positive` to `negative`<br/>Right-click: the tag changes it's value from `neutral` to `negative` to `positive` |
| **Exceptions and special cases:** |                                                                                                                                                                          |

### 2.2 Editing Catalogs

|                         **2.2.1** | **Adding a catalog**                                                                                                    |
| --------------------------------: | :---------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the catalog menu is opened                                                                                              |
|                      **Actions:** | 1. Click the "Add Catalog" button<br/>2. Insert data for the catalog<br/>3. Confirm the changes                         |
|                       **Result:** | 1. An edit dialog is opened<br/>2. The data is changed<br/>3. The dialog is closed and the catalog is created           |
| **Exceptions and special cases:** | The creation can also be cancelled<br/>If it is the first catalog to be created, the catalog is automatically activated |

|                         **2.2.2** | **Updating a catalog**                                                                                                       |
| --------------------------------: | :--------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the catalog menu is opened and at least one catalog exists                                                                   |
|                      **Actions:** | 1. Select a catalog in the list<br/>2. Click the "Edit" button<br/>3. Insert data for the catalog<br/>4. Confirm the changes |
|                       **Result:** | 1. -<br/>2. The edit dialog is opened<br/>3. The data is changed<br/>4. The dialog is closed and the catalog is updated      |
| **Exceptions and special cases:** | The update can also be cancelled                                                                                             |

|                         **2.2.3** | **Deleting a catalog**                                                                                                                                       |
| --------------------------------: | :----------------------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the catalog menu is opened and at least one catalog exists                                                                                                   |
|                      **Actions:** | 1. Select a catalog in the list<br/>2. Click the "Delete" button<br/>3. Confirm the deletion confirmation                                                    |
|                       **Result:** | The catalog is deleted                                                                                                                                       |
| **Exceptions and special cases:** | The deletion can also be cancelled<br/>If the catalog was active, the first catalog in the list is selected as the active one in case another catalog exists |

|                         **2.2.4** | **Importing a catalog**                                                                         |
| --------------------------------: | :---------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the catalog menu is opened                                                                      |
|                      **Actions:** | 1. Click the "Add Catalog" button<br/>2. Click the "Import" button<br/>3. Select an import file |
|                       **Result:** | The dialog is closed and the catalog is created                                                 |
| **Exceptions and special cases:** | The import can also be cancelled                                                                |

|                         **2.2.5** | **Exporting a catalog**                                                                                                                                                                                                                |
| --------------------------------: | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the catalog menu is opened and at least one catalog exists                                                                                                                                                                             |
|                      **Actions:** | 1. Select a catalog in the list<br/>2. Click the "Export" button<br/>3. Select an export file                                                                                                                                          |
|                       **Result:** | The catalog is exported                                                                                                                                                                                                                |
| **Exceptions and special cases:** | The export can also be cancelled<br/>It is also possible to select an export type for the mobile version of the application that exports a selection of the stored attributes to a file that can be imported in the mobile application |

### 2.3 Selecting Catalogs

|                         **2.3.1** | **Switching to a catalog**                                                            |
| --------------------------------: | :------------------------------------------------------------------------------------ |
|                 **Precondition:** | the catalog menu is opened and at least one inactive catalog exists                   |
|                      **Actions:** | 1. Double-click in inactive catalog in the list                                       |
|                       **Result:** | The catalog is actived                                                                |
| **Exceptions and special cases:** | In case another catalog was active before, the old active catalog will be deactivated |

### 2.4 Editing Media

|                         **2.4.1** | **Adding a medium**                                                         |
| --------------------------------: | :-------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened                                                 |
|                      **Actions:** | 1. Click the "Add Medium" button<br/>2. Insert data<br/>3. Save the changes |
|                       **Result:** | The medium is created                                                       |
| **Exceptions and special cases:** | The creation can also be cancelled                                          |

|                         **2.4.2** | **Updating a medium**                  |
| --------------------------------: | :------------------------------------- |
|                 **Precondition:** | the edit menu is opened                |
|                      **Actions:** | 1. Insert data<br/>2. Save the changes |
|                       **Result:** | The medium is updated                  |
| **Exceptions and special cases:** | The update can also be cancelled       |

|                         **2.4.3** | **Deleting a medium**                                                                       |
| --------------------------------: | :------------------------------------------------------------------------------------------ |
|                 **Precondition:** | the edit menu is opened                                                                     |
|                      **Actions:** | 1. Click the "Delete" button<br/>2. If configured, confirm the deletion confirmation dialog |
|                       **Result:** | The medium is deleted                                                                       |
| **Exceptions and special cases:** | If the deletion confirmation dialog is active, the deletion can also be cancelled           |

### 2.5 Editing Media Parts

|                         **2.5.1** | **Adding a media part**                                                         |
| --------------------------------: | :------------------------------------------------------------------------------ |
|                 **Precondition:** | the edit menu is opened                                                         |
|                      **Actions:** | 1. Click the "Add Media Part" button<br/>2. Insert data<br/>3. Save the changes |
|                       **Result:** | The media part is created                                                       |
| **Exceptions and special cases:** | The creation can also be cancelled                                              |

|                         **2.5.2** | **Updating a media part**                                                                                  |
| --------------------------------: | :--------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the edit menu is opened and at least one media part exists                                                 |
|                      **Actions:** | 1. Select the media part to edit<br/>2. Click the "Edit" button<br/>3. Insert data<br/>4. Save the changes |
|                       **Result:** | The media part is updated                                                                                  |
| **Exceptions and special cases:** | The update can also be cancelled                                                                           |

|                         **2.5.3** | **Deleting a media part**                                                                                                          |
| --------------------------------: | :--------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the edit menu is opened and at least one media part exists                                                                         |
|                      **Actions:** | 1. Select the media part to delete<br/>2. Click the "Delete" button<br/>3. If configured, confirm the deletion confirmation dialog |
|                       **Result:** | The media part is deleted                                                                                                          |
| **Exceptions and special cases:** | If the deletion confirmation dialog is active, the deletion can also be cancelled                                                  |

### 2.6 Editing Tags

|                         **2.6.1** | **Adding a tag**                                                               |
| --------------------------------: | :----------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu or tag menu is opened                                        |
|                      **Actions:** | 1. Click the "Add Tag" button<br/>2. Insert a tag name<br/>3. Save the changes |
|                       **Result:** | The tag is created                                                             |
| **Exceptions and special cases:** | The creation can also be cancelled                                             |

|                         **2.6.2** | **Renaming a tag**                                                                                            |
| --------------------------------: | :------------------------------------------------------------------------------------------------------------ |
|                 **Precondition:** | the tag menu is opened and at least one tag exists                                                            |
|                      **Actions:** | 1. Select the tag to rename<br/>2. Click the "Rename" button<br/>3. Insert a tag name<br/>4. Save the changes |
|                       **Result:** | The tag is renamed                                                                                            |
| **Exceptions and special cases:** | The name update can also be cancelled                                                                         |

|                         **2.6.3** | **Deleting a tag**                                                                                                          |
| --------------------------------: | :-------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the tag menu is opened and at least one tag exists                                                                          |
|                      **Actions:** | 1. Select the tag to delete<br/>2. Click the "Delete" button<br/>3. If configured, confirm the deletion confirmation dialog |
|                       **Result:** | The tag is deleted                                                                                                          |
| **Exceptions and special cases:** | If the deletion confirmation dialog is active, the deletion can also be cancelled                                           |

|                         **2.6.4** | **Editing tags of media and their parts**                                                                                                                |
| --------------------------------: | :------------------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the tag menu is opened and at least one tag and media with parts exist                                                                                   |
|                      **Actions:** | 1. Select a tag<br/>2. Select a medium<br/>3. Select the medium itself or a part on the medium<br/>4. Change the assigned values<br/>5. Save the changes |
|                       **Result:** | The changed tag values are updated                                                                                                                       |
| **Exceptions and special cases:** | The update can also be cancelled                                                                                                                         |

### 2.7 Searching Media

|                         **2.7.1** | **Search by Text**                                                              |
| --------------------------------: | :------------------------------------------------------------------------------ |
|                 **Precondition:** | the overview menu is opened                                                     |
|                      **Actions:** | 1. Insert a search text in the text area<br/>2. Start the search or press enter |
|                       **Result:** | The search result is updated accordingly                                        |
| **Exceptions and special cases:** |                                                                                 |

|                         **2.7.2** | **Search by Tags**                                             |
| --------------------------------: | :------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened                                    |
|                      **Actions:** | 1. Select tags in the search tag input<br/>2. Start the search |
|                       **Result:** | The search result is updated accordingly                       |
| **Exceptions and special cases:** |                                                                |

### 2.8 Changing Settings

|                         **2.8.1** | **Changing the language**                                                    |
| --------------------------------: | :--------------------------------------------------------------------------- |
|                 **Precondition:** | the settings menu is opened                                                  |
|                      **Actions:** | 1. Select a language from the dropdown<br/>2. Save or apply the new settings |
|                       **Result:** | The language is changed                                                      |
| **Exceptions and special cases:** | Changing the settings can also be cancelled                                  |

|                         **2.8.2** | **Changing the result list length**                                                    |
| --------------------------------: | :------------------------------------------------------------------------------------- |
|                 **Precondition:** | the settings menu is opened                                                            |
|                      **Actions:** | 1. Select a result list length from the dropdown<br/>2. Save or apply the new settings |
|                       **Result:** | The result list length is changed                                                      |
| **Exceptions and special cases:** | Changing the settings can also be cancelled                                            |

|                         **2.8.3** | **Configuring visible features for the overview menu**                                                                          |
| --------------------------------: | :------------------------------------------------------------------------------------------------------------------------------ |
|                 **Precondition:** | the settings menu is opened                                                                                                     |
|                      **Actions:** | 1. Enable or disable a feature in the list<br/>2. Save or apply the new settings                                                |
|                       **Result:** | The selected features are enabled and the other features are disabled<br/>This effects the multi use control on the main screen |
| **Exceptions and special cases:** | Changing the settings can also be cancelled                                                                                     |

### 2.9 Analyzing Data

|                         **2.9.1** | **Finding default media**                                                           |
| --------------------------------: | :---------------------------------------------------------------------------------- |
|                 **Precondition:** | the analysis menu is opened and the according option selected                       |
|                      **Actions:** | 1. Select a medium from the result list<br/>2. Edit the medium and save the changes |
|                       **Result:** | The result list is updated                                                          |
| **Exceptions and special cases:** | There may not be any results                                                        |

|                         **2.9.2** | **Finding media with unset general tags**                                           |
| --------------------------------: | :---------------------------------------------------------------------------------- |
|                 **Precondition:** | the analysis menu is opened and the according option selected                       |
|                      **Actions:** | 1. Select a medium from the result list<br/>2. Edit the medium and save the changes |
|                       **Result:** | The result list is updated                                                          |
| **Exceptions and special cases:** | There may not be any results                                                        |

|                         **2.9.3** | **Find media with doubled names**                                                                                                                                                                      |
| --------------------------------: | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the analysis menu is opened and the according option selected                                                                                                                                          |
|                      **Actions:** | 1. Select a doubled media name from the result list<br/>2. Click the "Edit" button<br/>3. Select the media to merge<br/>4. Select the media information and media parts to use<br/>5. Save the changes |
|                       **Result:** | The media are merged and the result list is updated                                                                                                                                                    |
| **Exceptions and special cases:** | There may not be any results                                                                                                                                                                           |

|                         **2.9.4** | **Find parts without any tags**                                                             |
| --------------------------------: | :------------------------------------------------------------------------------------------ |
|                 **Precondition:** | the analysis menu is opened and the according option selected                               |
|                      **Actions:** | 1. Select a media part from the result list<br/>2. Edit the media part and save the changes |
|                       **Result:** | The result list is updated                                                                  |
| **Exceptions and special cases:** | There may not be any results                                                                |

|                         **2.9.5** | **Find parts without length**                                                               |
| --------------------------------: | :------------------------------------------------------------------------------------------ |
|                 **Precondition:** | the analysis menu is opened and the according option selected                               |
|                      **Actions:** | 1. Select a media part from the result list<br/>2. Edit the media part and save the changes |
|                       **Result:** | The result list is updated                                                                  |
| **Exceptions and special cases:** | There may not be any results                                                                |

|                         **2.9.6** | **Find parts without publication year**                                                     |
| --------------------------------: | :------------------------------------------------------------------------------------------ |
|                 **Precondition:** | the analysis menu is opened and the according option selected                               |
|                      **Actions:** | 1. Select a media part from the result list<br/>2. Edit the media part and save the changes |
|                       **Result:** | The result list is updated                                                                  |
| **Exceptions and special cases:** | There may not be any results                                                                |

### 2.10 Editing Playlists

|                        **2.10.1** | **Viewing playlists**                    |
| --------------------------------: | :--------------------------------------- |
|                 **Precondition:** | the overview menu is opened              |
|                      **Actions:** | 1. Select a playlist in the playlist tab |
|                       **Result:** | The titles in the playlist are shown     |
| **Exceptions and special cases:** |                                          |

|                        **2.10.2** | **Creating a playlist**                                                                                                 |
| --------------------------------: | :---------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened                                                                                             |
|                      **Actions:** | 1. Select the "New Playlist" slot in the playlist control<br/>2. Insert a name for the playlist<br/>3. Add the playlist |
|                       **Result:** | The playlist is created                                                                                                 |
| **Exceptions and special cases:** |                                                                                                                         |

|                        **2.10.3** | **Generating a random playlist**                                                                                                              |
| --------------------------------: | :-------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened                                                                                                                   |
|                      **Actions:** | 1. Select the "New Playlist" slot in the playlist control<br/>2. Select a playlist length<br/>3. Select a tag and click the "Generate" button |
|                       **Result:** | The playlist is created and filled with random media parts according to the selected tag and length                                           |
| **Exceptions and special cases:** | The playlist may be shorter then the selected length if no more media parts exist for the tag                                                 |

|                        **2.10.4** | **Adding elements to a playlist**                                                                                                      |
| --------------------------------: | :------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened, there exists one playlist and there exist media with parts                                                |
|                      **Actions:** | 1. Search titles within the search control<br/>2. Click the playlist button next to an item to add it to the currently opened playlist |
|                       **Result:** | The playlist is updated                                                                                                                |
| **Exceptions and special cases:** |                                                                                                                                        |

|                        **2.10.5** | **Removing elements from a playlist**                                                         |
| --------------------------------: | :-------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened and there exists one playlist                                     |
|                      **Actions:** | 1. Select a playlist within the playlist control<br/>2. Click the "X" button next to an entry |
|                       **Result:** | The entry is removed                                                                          |
| **Exceptions and special cases:** |                                                                                               |

|                        **2.10.6** | **Updating a playlist**                                                                         |
| --------------------------------: | :---------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened and there exists one playlist                                       |
|                      **Actions:** | 1. Select a playlist within the playlist control<br/>2. Add or remove items within the playlist |
|                       **Result:** | The playlist is updated                                                                         |
| **Exceptions and special cases:** |                                                                                                 |

|                        **2.10.7** | **Deleting a playlist**                                                                                                                          |
| --------------------------------: | :----------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened and there exists one playlist                                                                                        |
|                      **Actions:** | 1. Select a playlist within the playlist control<br/>2. Click the "Delete" button<br/>3. If configured, confirm the deletion confirmation dialog |
|                       **Result:** | The playlist is deleted                                                                                                                          |
| **Exceptions and special cases:** | If the deletion confirmation dialog is active, the deletion can also be cancelled                                                                |

### 2.11 Viewing the Title of the Day

|                        **2.11.1** | **Viewing the title of the day**                                                                                                          |
| --------------------------------: | :---------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened and media with parts exist                                                                                    |
|                      **Actions:** | 1. Select the title of the day control                                                                                                    |
|                       **Result:** | The current title of the day is shown                                                                                                     |
| **Exceptions and special cases:** | If no media exist, no title of the day can be shown<br/>The control can also be configured to show a whole medium instead of a media part |

|                        **2.11.2** | **Selecting a new title of the day**                                                                               |
| --------------------------------: | :----------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened and media with parts exist                                                             |
|                      **Actions:** | 1. Click the "Next" button in the title of the day control                                                         |
|                       **Result:** | Another title is shown                                                                                             |
| **Exceptions and special cases:** | The currently shown title may not change if no further titles exist or if the same title is rolled again by chance |

### 2.12 Viewing Statistics

|                        **2.12.1** | **Viewing the statistics**                                                                                 |
| --------------------------------: | :--------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened                                                                                |
|                      **Actions:** | 1. Select the statistics control                                                                           |
|                       **Result:** | Overview statistics are shown regarding the media and their parts (such as count of media and their parts) |
| **Exceptions and special cases:** |                                                                                                            |

### 2.13 Opening the Help Menu

|                        **2.13.1** | **Opening the help menu**                                              |
| --------------------------------: | :--------------------------------------------------------------------- |
|                 **Precondition:** |                                                                        |
|                      **Actions:** | 1. Click the "Help" button                                             |
|                       **Result:** | The help menu is opened                                                |
| **Exceptions and special cases:** | If an instance of the help menu is already opened, it will be focussed |

|                        **2.13.2** | **Navigating through the help menu**                                                             |
| --------------------------------: | :----------------------------------------------------------------------------------------------- |
|                 **Precondition:** |                                                                                                  |
|                      **Actions:** | 1. Select a topic in the lefthand list<br/>2. Navigate through the pages using the arrow buttons |
|                       **Result:** | The menu navigates through the manual and shows screenshows and explanations accordingly         |
| **Exceptions and special cases:** |                                                                                                  |

## 3 Application Screens

The _Media Manager_ supports the user with a clear UI design that highlights the most important actions using individual icons. That way, the user can intuitively use the application.

### 3.x ...

## 4 Stored Data

The application stores the data within a local MSSQL database.

This data will only be stored locally as long as the user does not copy it manually. The user must ensure to keep the device safe and secure the device access appropriately to keep the data safe.

## 5 Definitions

### 5.1 Types

#### 5.1.1 Catalogue Attributes

Catalogues contain the following data:

|                              Attribute | Data Type  | Explanation                                                                           |
| -------------------------------------: | :--------- | :------------------------------------------------------------------------------------ |
|                                     Id | Numeric    | Identification of the catalogue                                                       |
|                                  Title | Text       | Title of the catalogue                                                                |
|                            Description | Text       | Detailed description of the catalogue                                                 |
|           Deletion Confirmation Medium | True/False | Whether the application asks for a deletion confirmation when deleting a whole medium |
|             Deletion Confirmation Part | True/False | Whether the application asks for a deletion confirmation when deleting a media part   |
|              Deletion Confirmation Tag | True/False | Whether the application asks for a deletion confirmation when deleting a tag          |
|         Deletion Confirmation Playlist | True/False | Whether the application asks for a deletion confirmation when deleting a playlist     |
| Show whole medium for title of the day | True/false | Whether to show a media part or a whole medium within the title of the day control    |

#### 5.1.2 Media Attributes

Media contain the following data:

|    Attribute | Data Type    | Explanation                               |
| -----------: | :----------- | :---------------------------------------- |
|           Id | Numeric      | Identification of the medium              |
| Catalogue Id | Numeric      | Reference to the parent catalogue         |
|        Title | Text         | Title of the medium                       |
|  Description | Text         | Detailed description of the medium        |
|     Location | Text         | Place where the medium is located         |
|         Tags | List of Tags | Categories the medium is contained within |

#### 5.1.3 Media Part Attributes

Each media can hold several parts.

Media parts contain the following data:

|        Attribute | Data Type    | Explanation                               |
| ---------------: | :----------- | :---------------------------------------- |
|               Id | Numeric      | Identification of the part                |
|        Medium Id | Numeric      | Reference to the parent medium            |
|            Title | Text         | Title of the part                         |
|      Description | Text         | Detailed description of the part          |
|        Favourite | True/False   | Whether the part is a favourite           |
|           Length | Numeric      | Length of the part                        |
| Publication Year | Numeric      | Publication year of the part              |
|            Image | Image        | Cover image or thumbnail of the part      |
|             Tags | List of Tags | Categories the medium is contained within |

If a tag for the whole media has a set value (not neutral), all media parts on this media will have the same value for that tag. Media parts can only redefine the neutral tags of the parent media.

#### 5.1.4 Tag

Each tag can have one of the follow values:

- neutral: no data inserted for the category
- positive: the media or part is contained within the category
- negative: the media or part is not contained within the category

Configured tags contain the following data:

|    Attribute | Data Type | Explanation                       |
| -----------: | :-------- | :-------------------------------- |
|           Id | Numeric   | Identification of the tag         |
| Catalogue Id | Numeric   | Reference to the parent catalogue |
|        Title | Text      | Title of the tag                  |

### 5.2 Algorithms

#### 5.2.1 Search Parameters

| Parameter               | Explanation                                                                                                                |
| :---------------------- | :------------------------------------------------------------------------------------------------------------------------- |
| Search Text             | String to use for the applied search                                                                                       |
| Tags                    | Tags to use for the applied search                                                                                         |
| Exact Mode              | In exact mode, the ticked tags must match exactly; otherwise the ticked tags cannot have the opposite value                |
| Search Description Mode | Whether the text should also be searched within the descriptions; otherwise only the media and part titles will be scanned |
| Favourite Only Mode     | Filter out all parts that are not marked as favourite                                                                      |
| Result List Type        | Whether to show the results as a list of media or as a list of parts                                                       |

Default search parameters: By default, the search text is empty and no tag is selected. Furthermore no mode will be active and the result list will show the media.

## 6 Test Cases

### 6.x ...

|         **6.x.x** | **Title** |
| ----------------: | :-------- |
| **Precondition:** | ...       |
|      **Actions:** | 1. ...    |
|       **Result:** | (1.) ...  |

## 7 Appendix

The illustrations were created using either `draw.io` or a running instance of the application.
