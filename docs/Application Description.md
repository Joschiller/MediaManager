# Media Manager

<div style="background: #141414;" width="100%" height="640px">
  <img style="margin: 40%; padding-right: 64px" src="./styles/app_icon/icon.png" height="128px"/>
</div>

<div style="page-break-after: always;"></div>

- [1 Introduction](#1-introduction)
- [2 Use Cases](#2-use-cases)
  - [2.1 Showing Help](#21-showing-help)
  - [2.2 Using Inputs](#22-using-inputs)
  - [2.3 Editing Catalogs](#23-editing-catalogs)
  - [2.4 Selecting Catalogs](#24-selecting-catalogs)
  - [2.5 Editing Media](#25-editing-media)
  - [2.6 Editing Media Parts](#26-editing-media-parts)
  - [2.7 Editing Tags](#27-editing-tags)
  - [2.8 Searching Media](#28-searching-media)
  - [2.9 Changing Settings](#29-changing-settings)
  - [2.10 Analyzing Data](#210-analyzing-data)
  - [2.11 Editing Playlists](#211-editing-playlists)
  - [2.12 Viewing the Title of the Day](#212-viewing-the-title-of-the-day)
  - [2.13 Viewing Statistics](#213-viewing-statistics)
  - [2.14 Viewing Media](#214-viewing-media)
- [3 Application Screens](#3-application-screens)
  - [3.1 Help Menu](#31-help-menu)
  - [3.2 Catalog Menu](#32-catalog-menu)
  - [3.3 Overview Menu](#33-overview-menu)
  - [3.4 Edit Menu](#34-edit-menu)
  - [3.5 Tag Menu](#35-tag-menu)
  - [3.6 Settings Menu](#36-settings-menu)
  - [3.7 Analyze Menu](#37-analyze-menu)
- [4 Stored Data](#4-stored-data)
- [5 Definitions](#5-definitions)
  - [5.1 Types](#51-types)
    - [5.1.1 Catalog Attributes](#511-catalog-attributes)
    - [5.1.2 Media Attributes](#512-media-attributes)
    - [5.1.3 Media Part Attributes](#513-media-part-attributes)
    - [5.1.4 Tag Attributes](#514-tag-attributes)
    - [5.1.5 Playlist Attributes](#515-playlist-attributes)
    - [5.1.6 Settings](#516-settings)
  - [5.2 Algorithms](#52-algorithms)
    - [5.2.1 Search Parameters](#521-search-parameters)
    - [5.2.2 Export and Automatic Backup](#522-export-and-automatic-backup)
    - [5.2.3 Analyze Results](#523-analyze-results)
- [6 Test Cases](#6-test-cases)
  - [6.1 Showing Help](#61-showing-help)
  - [6.2 Editing Catalogs](#62-editing-catalogs)
  - [6.3 Editing and Viewing Media and Media Parts](#63-editing-and-viewing-media-and-media-parts)
  - [6.4 Editing Tags](#64-editing-tags)
  - [6.5 Searching Media and Media Parts](#65-searching-media-and-media-parts)
  - [6.6 Changing Settings](#66-changing-settings)
  - [6.7 Analyzing Data](#67-analyzing-data)
  - [6.8 Editing Playlists](#68-editing-playlists)
- [7 Appendix](#7-appendix)

## 1 Introduction

The _Media Manager_ application can be used to list different types of media with all their contents. Each media and media part can be categorized using tags as well as other metadata that can be used to search for specific items within the application. The application offers a searching functionality as well as several other support features that enhance the creation of media catalogs.

## 2 Use Cases

### 2.1 Showing Help

|                         **2.1.1** | **Opening the help menu**                                                              |
| --------------------------------: | :------------------------------------------------------------------------------------- |
|                 **Precondition:** |                                                                                        |
|                      **Actions:** | 1. Click the "Help"-Button in the menu bar                                             |
|                       **Result:** | The help menu is opened, containing different topics that lead through the application |
| **Exceptions and special cases:** | If an instance of the help menu is already opened, it will be focused                  |

|                         **2.1.2** | **Navigating through the help menu**                                                                           |
| --------------------------------: | :------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** |                                                                                                                |
|                      **Actions:** | 1. Select a topic in the lefthand list<br/>2. Navigate through the pages using the arrow buttons or arrow keys |
|                       **Result:** | The menu navigates through the manual and shows screenshots and explanations accordingly                       |
| **Exceptions and special cases:** |                                                                                                                |

### 2.2 Using Inputs

|                         **2.2.1** | **Using a tag input**                                                                                                                                                                                                        |
| --------------------------------: | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** |                                                                                                                                                                                                                              |
|                      **Actions:** | 1. Left-click or right-click the checkbox                                                                                                                                                                                    |
|                       **Result:** | Left-click: the tag changes its value from `neutral` to `positive` to `negative` and then back to `neutral`<br/>Right-click: the tag changes its value from `neutral` to `negative` to `positive` and then back to `neutral` |
| **Exceptions and special cases:** |                                                                                                                                                                                                                              |

### 2.3 Editing Catalogs

|                         **2.3.1** | **Adding a catalog**                                                                                                    |
| --------------------------------: | :---------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the catalog menu is opened                                                                                              |
|                      **Actions:** | 1. Click the "Add Catalog" button<br/>2. Insert data for the catalog<br/>3. Confirm the changes                         |
|                       **Result:** | 1. An edit dialog is opened<br/>2. The data is changed<br/>3. The dialog is closed and the catalog is created           |
| **Exceptions and special cases:** | The creation can also be cancelled<br/>If it is the first catalog to be created, the catalog is automatically activated |

|                         **2.3.2** | **Updating a catalog**                                                                                                                               |
| --------------------------------: | :--------------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the catalog menu is opened and at least one catalog exists                                                                                           |
|                      **Actions:** | 1. Select a catalog in the list<br/>2. Click the "Edit Catalog" button<br/>3. Insert data for the catalog<br/>4. Confirm the changes                 |
|                       **Result:** | 1. The "Edit" button is available<br/>2. The edit dialog is opened<br/>3. The data is changed<br/>4. The dialog is closed and the catalog is updated |
| **Exceptions and special cases:** | The update can also be cancelled                                                                                                                     |

|                         **2.3.3** | **Deleting a catalog**                                                                                                                                       |
| --------------------------------: | :----------------------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the catalog menu is opened and at least one catalog exists                                                                                                   |
|                      **Actions:** | 1. Select a catalog in the list<br/>2. Click the "Delete Catalog" button<br/>3. Confirm the deletion confirmation                                            |
|                       **Result:** | The catalog is deleted                                                                                                                                       |
| **Exceptions and special cases:** | The deletion can also be cancelled<br/>If the catalog was active, the first catalog in the list is selected as the active one in case another catalog exists |

|                         **2.3.4** | **Exporting a catalog**                                                                                                                                                                                                                                                                                           |
| --------------------------------: | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the catalog menu is opened and at least one catalog exists                                                                                                                                                                                                                                                        |
|                      **Actions:** | 1. Select a catalog in the list<br/>2. Click the "Export Catalog" button<br/>3. Select an export file                                                                                                                                                                                                             |
|                       **Result:** | The catalog is exported                                                                                                                                                                                                                                                                                           |
| **Exceptions and special cases:** | The export can also be cancelled<br/>It is also possible to select an export type for the mobile version of the application that exports a selection of the stored attributes to a file that can be imported in the mobile application<br/><span style="color: red">The mobile export is not available yet</span> |

|                         **2.3.5** | **Importing a catalog**                                                                                               |
| --------------------------------: | :-------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the catalog menu is opened                                                                                            |
|                      **Actions:** | 1. Click the "Import Catalog" button<br/>2. Select an import file                                                     |
|                       **Result:** | The catalog is imported as a new catalog                                                                              |
| **Exceptions and special cases:** | The import can also be cancelled<br/>If it is the first catalog to be created, the catalog is automatically activated |

### 2.4 Selecting Catalogs

|                         **2.4.1** | **Switching to a catalog**                                                                                                                           |
| --------------------------------: | :--------------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the catalog menu is opened and at least one inactive catalog exists                                                                                  |
|                      **Actions:** | 1. Double-click in inactive catalog in the list                                                                                                      |
|                       **Result:** | The catalog is activated<br/>If the setting is not deactivated, both the now deactivated and now activated catalog will be exported to a backup file |
| **Exceptions and special cases:** | In case another catalog was active before, the old active catalog will be deactivated                                                                |

### 2.5 Editing Media

|                         **2.5.1** | **Adding a medium**                                                                         |
| --------------------------------: | :------------------------------------------------------------------------------------------ |
|                 **Precondition:** | the overview menu is opened                                                                 |
|                      **Actions:** | 1. Click the "Add Medium" button<br/>2. Insert data<br/>3. Save the changes                 |
|                       **Result:** | 1. The edit menu is opened<br/>2. The data is changed<br/>3. The medium is created          |
| **Exceptions and special cases:** | The creation can also be cancelled<br/>If no title is inserted, the changes cannot be saved |

|                         **2.5.2** | **Updating a medium**                                                                     |
| --------------------------------: | :---------------------------------------------------------------------------------------- |
|                 **Precondition:** | the edit menu is opened                                                                   |
|                      **Actions:** | 1. Click the "Edit Medium" button<br/>2. Insert data<br/>3. Save the changes              |
|                       **Result:** | The medium is updated                                                                     |
| **Exceptions and special cases:** | The update can also be cancelled<br/>If no title is inserted, the changes cannot be saved |

|                         **2.5.3** | **Deleting a medium**                                                                              |
| --------------------------------: | :------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the edit menu is opened                                                                            |
|                      **Actions:** | 1. Click the "Delete Medium" button<br/>2. If configured, confirm the deletion confirmation dialog |
|                       **Result:** | The medium is deleted                                                                              |
| **Exceptions and special cases:** | If the deletion confirmation dialog is active, the deletion can also be cancelled                  |

### 2.6 Editing Media Parts

|                         **2.6.1** | **Adding a media part**                              |
| --------------------------------: | :--------------------------------------------------- |
|                 **Precondition:** | the edit menu is opened in edit mode                 |
|                      **Actions:** | 1. Click the "Add Media Part" button                 |
|                       **Result:** | An empty part is added to the medium                 |
| **Exceptions and special cases:** | If no title is inserted, the changes cannot be saved |

|                         **2.6.2** | **Updating a media part**                                               |
| --------------------------------: | :---------------------------------------------------------------------- |
|                 **Precondition:** | the edit menu is opened in edit mode and at least one media part exists |
|                      **Actions:** | 1. Select the media part to edit<br/>2. Insert data                     |
|                       **Result:** | The media part is updated                                               |
| **Exceptions and special cases:** | If no title is inserted, the changes cannot be saved                    |

|                         **2.6.3** | **Deleting a media part**                                                                                                                     |
| --------------------------------: | :-------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the edit menu is opened in edit mode and at least one media part exists                                                                       |
|                      **Actions:** | 1. Select the media part to delete<br/>2. Click the "Delete Media Part" button<br/>3. If configured, confirm the deletion confirmation dialog |
|                       **Result:** | The media part is deleted                                                                                                                     |
| **Exceptions and special cases:** | If the deletion confirmation dialog is active, the deletion can also be cancelled                                                             |

### 2.7 Editing Tags

|                         **2.7.1** | **Adding a tag**                                                               |
| --------------------------------: | :----------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu or tag menu is opened                                        |
|                      **Actions:** | 1. Click the "Add Tag" button<br/>2. Insert a tag name<br/>3. Save the changes |
|                       **Result:** | The tag is created                                                             |
| **Exceptions and special cases:** | The creation can also be cancelled                                             |

|                         **2.7.2** | **Renaming a tag**                                                                                              |
| --------------------------------: | :-------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the tag menu is opened and at least one tag exists                                                              |
|                      **Actions:** | 1. Select the tag to rename<br/>2. Click the "Edit Tag" button<br/>3. Insert a tag name<br/>4. Save the changes |
|                       **Result:** | The tag is renamed                                                                                              |
| **Exceptions and special cases:** | The name update can also be cancelled                                                                           |

|                         **2.7.3** | **Deleting a tag**                                                                                                              |
| --------------------------------: | :------------------------------------------------------------------------------------------------------------------------------ |
|                 **Precondition:** | the tag menu is opened and at least one tag exists                                                                              |
|                      **Actions:** | 1. Select the tag to delete<br/>2. Click the "Delete Tag" button<br/>3. If configured, confirm the deletion confirmation dialog |
|                       **Result:** | The tag is deleted                                                                                                              |
| **Exceptions and special cases:** | If the deletion confirmation dialog is active, the deletion can also be cancelled                                               |

|                         **2.7.4** | **Editing tags of media and their parts**                                                                                    |
| --------------------------------: | :--------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the tag menu is opened and at least one tag and media with parts exist                                                       |
|                      **Actions:** | 1. Select a tag<br/>2. Select a medium<br/>3. Change the assigned values for the medium or its parts<br/>4. Save the changes |
|                       **Result:** | The changed tag values are updated                                                                                           |
| **Exceptions and special cases:** | The update can also be cancelled                                                                                             |

### 2.8 Searching Media

|                         **2.8.1** | **Search by Text**                                                              |
| --------------------------------: | :------------------------------------------------------------------------------ |
|                 **Precondition:** | the overview menu is opened                                                     |
|                      **Actions:** | 1. Insert a search text in the text area<br/>2. Start the search or press enter |
|                       **Result:** | The search result is updated accordingly                                        |
| **Exceptions and special cases:** |                                                                                 |

|                         **2.8.2** | **Search by Tags**                                             |
| --------------------------------: | :------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened                                    |
|                      **Actions:** | 1. Select tags in the search tag input<br/>2. Start the search |
|                       **Result:** | The search result is updated accordingly                       |
| **Exceptions and special cases:** |                                                                |

### 2.9 Changing Settings

|                         **2.9.1** | **Changing the language**                                                    |
| --------------------------------: | :--------------------------------------------------------------------------- |
|                 **Precondition:** | the settings menu is opened                                                  |
|                      **Actions:** | 1. Select a language from the dropdown<br/>2. Save or apply the new settings |
|                       **Result:** | The language is changed                                                      |
| **Exceptions and special cases:** | Changing the settings can also be cancelled                                  |

|                         **2.9.2** | **Configuring visible features for the overview menu**                                                                          |
| --------------------------------: | :------------------------------------------------------------------------------------------------------------------------------ |
|                 **Precondition:** | the settings menu is opened                                                                                                     |
|                      **Actions:** | 1. Enable or disable a feature in the list<br/>2. Save or apply the new settings                                                |
|                       **Result:** | The selected features are enabled and the other features are disabled<br/>This effects the multi-use control on the main screen |
| **Exceptions and special cases:** | Changing the settings can also be cancelled                                                                                     |

|                         **2.9.3** | **Changing the result list length**                                                    |
| --------------------------------: | :------------------------------------------------------------------------------------- |
|                 **Precondition:** | the settings menu is opened                                                            |
|                      **Actions:** | 1. Select a result list length from the dropdown<br/>2. Save or apply the new settings |
|                       **Result:** | The result list length is changed                                                      |
| **Exceptions and special cases:** | Changing the settings can also be cancelled                                            |

|                         **2.9.4** | **Changing the backup configuration**                                                                                        |
| --------------------------------: | :--------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the settings menu is opened                                                                                                  |
|                      **Actions:** | 1. Change the folder path for the backup<br/>2. Enable or disable the automatic backup<br/>3. Save or apply the new settings |
|                       **Result:** | The backup configuration is changed                                                                                          |
| **Exceptions and special cases:** | Changing the settings can also be cancelled                                                                                  |

|                         **2.9.5** | **Enabling or disabling deletion confirmations**                                                                            |
| --------------------------------: | :-------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the settings menu is opened and at least one catalog exists                                                                 |
|                      **Actions:** | 1. Enable or disable the deletion confirmations Change the folder path for the backup<br/>2. Save or apply the new settings |
|                       **Result:** | The deletion confirmation settings are changed                                                                              |
| **Exceptions and special cases:** | Changing the settings can also be cancelled                                                                                 |

|                         **2.9.6** | **Changing the title of the day display**                                                           |
| --------------------------------: | :-------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the settings menu is opened and at least one catalog exists                                         |
|                      **Actions:** | 1. Change the mode in which the title of the day is displayed<br/>2. Save or apply the new settings |
|                       **Result:** | The title of the day configuration is changed                                                       |
| **Exceptions and special cases:** | Changing the settings can also be cancelled                                                         |

### 2.10 Analyzing Data

|                        **2.10.1** | **Finding empty media**                                                             |
| --------------------------------: | :---------------------------------------------------------------------------------- |
|                 **Precondition:** | the analysis menu is opened and the according option is selected                    |
|                      **Actions:** | 1. Select a medium from the result list<br/>2. Edit the medium and save the changes |
|                       **Result:** | The result list is updated                                                          |
| **Exceptions and special cases:** | There may not be any results                                                        |

|                        **2.10.2** | **Finding media with unset general tags**                                           |
| --------------------------------: | :---------------------------------------------------------------------------------- |
|                 **Precondition:** | the analysis menu is opened and the according option is selected                    |
|                      **Actions:** | 1. Select a medium from the result list<br/>2. Edit the medium and save the changes |
|                       **Result:** | The result list is updated                                                          |
| **Exceptions and special cases:** | There may not be any results                                                        |

|                        **2.10.3** | **Find media with doubled names**                                                                                                                                                                         |
| --------------------------------: | :-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the analysis menu is opened and the according option is selected                                                                                                                                          |
|                      **Actions:** | 1. Select a doubled media name from the result list<br/>2. Click the "Edit" button<br/>3. Select the media to merge<br/>4. Select the media information and media parts to use<br/>5. Confirm the preview |
|                       **Result:** | The selected media are merged and the result list is updated                                                                                                                                              |
| **Exceptions and special cases:** | There may not be any results                                                                                                                                                                              |

|                        **2.10.4** | **Find media or parts without a given attribute value**                                                       |
| --------------------------------: | :------------------------------------------------------------------------------------------------------------ |
|                 **Precondition:** | the analysis menu is opened and the according option is selected                                              |
|                      **Actions:** | 1. Select a media or media part from the result list<br/>2. Edit the media or media part and save the changes |
|                       **Result:** | The result list is updated                                                                                    |
| **Exceptions and special cases:** | There may not be any results                                                                                  |

### 2.11 Editing Playlists

|                        **2.11.1** | **Viewing playlists**                                         |
| --------------------------------: | :------------------------------------------------------------ |
|                 **Precondition:** | the overview menu is opened; the according setting is enabled |
|                      **Actions:** | 1. Select a playlist in the playlist tab                      |
|                       **Result:** | The titles in the playlist are shown                          |
| **Exceptions and special cases:** |                                                               |

|                        **2.11.2** | **Creating a playlist**                                                                                                                                                              |
| --------------------------------: | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened; the according setting is enabled                                                                                                                        |
|                      **Actions:** | 1. Click the "Add Playlist" button in the playlist control<br/>2. Insert a name for the playlist<br/>3. Optionally select a tag and playlist length<br/>4. Click the "Create" button |
|                       **Result:** | The playlist is created<br/>If a tag and length were inserted, the playlist is randomly filled with parts that match the given tag until the length is reached                       |
| **Exceptions and special cases:** | The playlist may be shorter than the selected length if no more media parts exist for the tag                                                                                        |

|                        **2.11.3** | **Adding elements to a playlist**                                                                                                                                                   |
| --------------------------------: | :---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened, there exists one playlist and there exist media with parts; the according setting is enabled                                                           |
|                      **Actions:** | 1. Search titles within the search control<br/>2. Right click within the search result list<br/>3. Select the add to playlist option<br/>4. Select a playlist to add the element to |
|                       **Result:** | The playlist is updated                                                                                                                                                             |
| **Exceptions and special cases:** |                                                                                                                                                                                     |

|                        **2.11.4** | **Removing elements from a playlist**                                                         |
| --------------------------------: | :-------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened and there exists one playlist; the according setting is enabled   |
|                      **Actions:** | 1. Select a playlist within the playlist control<br/>2. Click the "X" button next to an entry |
|                       **Result:** | The entry is removed                                                                          |
| **Exceptions and special cases:** |                                                                                               |

|                                                                                   **2.11.5** | **Deleting a playlist**                                                                     |
| -------------------------------------------------------------------------------------------: | :------------------------------------------------------------------------------------------ |
|                                                                            **Precondition:** | the overview menu is opened and there exists one playlist; the according setting is enabled |
| \*\*Actions: Playlist" button<br/>3. If configured, confirm the deletion confirmation dialog |
|                                                                                  **Result:** | The playlist is deleted                                                                     |
|                                                            **Exceptions and special cases:** | If the deletion confirmation dialog is active, the deletion can also be cancelled           |

### 2.12 Viewing the Title of the Day

|                        **2.12.1** | **Viewing the title of the day**                                                                                                          |
| --------------------------------: | :---------------------------------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened and media with parts exist; the according setting is enabled                                                  |
|                      **Actions:** | 1. Select the title of the day control                                                                                                    |
|                       **Result:** | The current title of the day is shown                                                                                                     |
| **Exceptions and special cases:** | If no media exist, no title of the day can be shown<br/>The control can also be configured to show a whole medium instead of a media part |

|                        **2.12.2** | **Selecting a new title of the day**                                                                               |
| --------------------------------: | :----------------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened and media with parts exist; the according setting is enabled                           |
|                      **Actions:** | 1. Click the "Next" button in the title of the day control                                                         |
|                       **Result:** | Another title is shown                                                                                             |
| **Exceptions and special cases:** | The currently shown title may not change if no further titles exist or if the same title is rolled again by chance |

### 2.13 Viewing Statistics

|                        **2.13.1** | **Viewing the statistics**                                                                                 |
| --------------------------------: | :--------------------------------------------------------------------------------------------------------- |
|                 **Precondition:** | the overview menu is opened; the according setting is enabled                                              |
|                      **Actions:** | 1. Select the statistics control                                                                           |
|                       **Result:** | Overview statistics are shown regarding the media and their parts (such as count of media and their parts) |
| **Exceptions and special cases:** |                                                                                                            |

### 2.14 Viewing Media

|                        **2.14.1** | **Opening a medium or part from the search result**         |
| --------------------------------: | :---------------------------------------------------------- |
|                 **Precondition:** | a medium or part is visible in the search result            |
|                      **Actions:** | 1. Double click the medium or part                          |
|                       **Result:** | The edit menu is opened showing the selected medium or part |
| **Exceptions and special cases:** |                                                             |

|                        **2.14.2** | **Opening a part from a playlist**                |
| --------------------------------: | :------------------------------------------------ |
|                 **Precondition:** | a part is visible in a playlist                   |
|                      **Actions:** | 1. Double click the part                          |
|                       **Result:** | The edit menu is opened showing the selected part |
| **Exceptions and special cases:** |                                                   |

## 3 Application Screens

The _Media Manager_ supports the user with a clear UI design that highlights the most important actions using individual icons. That way, the user can intuitively use the application.

### 3.1 Help Menu

The help menu can be reached from all other menus by using the "Help" button in the menu bar. The help menu shows different topics in the lefthand list from which the user can select a desired topic. The user can then navigate through the topics and pages of the topics by using the arrow buttons or the arrow keys.

![Help Menu](../HelpImages/HelpMenu.png)

### 3.2 Catalog Menu

Within the catalog menu, catalogs can be created, edited or deleted. Furthermore, the active catalog can be selected or catalogs can be exported and imported.

The catalog menu is the first menu that will be opened, if no catalog exists yet. The menu then offers the access to the catalog creation as well as the settings and help menu.

![Catalog Menu](../HelpImages/CatalogMenu.png)

### 3.3 Overview Menu

The overview menu provides the searching functionality on the righthand side of the menu as well as the access to all other menus.

On the lefthand side of the menu, further controls can be enabled that allow editing playlists, viewing the title of the day or viewing statistics.

To open an existing medium from the overview menu, an item in a playlist or the search result can be double clicked.

![Overview Menu](../HelpImages/OverviewMenu.png)

### 3.4 Edit Menu

Within the edit menu, media and their parts can be edited. When creating a new medium, the menu will automatically be opened in edit mode. Otherwise, the chosen medium will be shown in viewing mode and can be edited, by clicking the "Edit Medium" button in the menu bar.

Then the medium can be edited with all its parts. Changes must be saved explicitly.

![Edit Menu](../HelpImages/EditMenu.png)

### 3.5 Tag Menu

The tag menu lists all tags that exist within the application and offers the editing and deletion functionality for those tags.

By selecting a tag and a medium within the menu, the tag values for this medium and its parts can be edited on the righthand side of the menu. Changes must be saved explicitly.

![Tag Menu](../HelpImages/TagMenu_Medium.png)

### 3.6 Settings Menu

Within the settings menu, several tabs allow the access to different configurations. The catalog settings are only available if any catalog exists. Changes must be saved explicitly.

![Settings Menu Backup](../HelpImages/SettingsMenu_Backup.png)
![Settings Menu Global](../HelpImages/SettingsMenu_Global.png)
![Settings Menu Catalog](../HelpImages/SettingsMenu_Catalog.png)

### 3.7 Analyze Menu

The analyze menu provides the access to different inconsistencies that were found within the currently active catalog. This includes empty or doubled media as well as missing attributes for media or their parts.

The found items are shown in a preview and can then be opened in the edit menu by clicking the edit button.

In case of doubled media, the items are not edited in the edit menu. Rather they are opened in a merge menu that allows the combination of the desired media information and parts to create a single combined medium instead of the doubled items.

![Analyze Menu](../HelpImages/AnalyzeMenu.png)
![Merge Menu](../HelpImages/AnalyzeMenu_MergeEdit.png)

## 4 Stored Data

The application stores the data within a local MSSQL database.

This data will only be stored locally as long as the user does not copy it manually. The user must ensure to keep the device safe and secure the device access appropriately to keep the data safe.

If desired, the user can export the data to xml-Files outside the application. This can be used to transfer data between different devices or to create a backup.

## 5 Definitions

### 5.1 Types

An overview of the data types can be viewed in the following diagram:

![ERD](../ERD.png)

#### 5.1.1 Catalog Attributes

Catalogs contain the following data:

|                              Attribute | Data Type              | Explanation                                                                           |
| -------------------------------------: | :--------------------- | :------------------------------------------------------------------------------------ |
|                                     Id | Numeric                | Identification of the catalog                                                         |
|                                  Title | Text (128 characters)  | Title of the catalog                                                                  |
|                            Description | Text (2048 characters) | Detailed description of the catalog                                                   |
|           Deletion Confirmation Medium | True/False             | Whether the application asks for a deletion confirmation when deleting a whole medium |
|             Deletion Confirmation Part | True/False             | Whether the application asks for a deletion confirmation when deleting a media part   |
|              Deletion Confirmation Tag | True/False             | Whether the application asks for a deletion confirmation when deleting a tag          |
|         Deletion Confirmation Playlist | True/False             | Whether the application asks for a deletion confirmation when deleting a playlist     |
| Show whole medium for title of the day | True/false             | Whether to show a media part or a whole medium within the title of the day control    |

#### 5.1.2 Media Attributes

Media contain the following data:

|   Attribute | Data Type             | Explanation                               |
| ----------: | :-------------------- | :---------------------------------------- |
|          Id | Numeric               | Identification of the medium              |
|  Catalog Id | Numeric               | Reference to the parent catalog           |
|       Title | Text (128 characters) | Title of the medium                       |
| Description | Text (512 characters) | Detailed description of the medium        |
|    Location | Text (128 characters) | Place where the medium is located         |
|        Tags | List of Tags          | Categories the medium is contained within |

#### 5.1.3 Media Part Attributes

Each media can hold several parts.

Media parts contain the following data:

|        Attribute | Data Type             | Explanation                             |
| ---------------: | :-------------------- | :-------------------------------------- |
|               Id | Numeric               | Identification of the part              |
|        Medium Id | Numeric               | Reference to the parent medium          |
|            Title | Text (128 characters) | Title of the part                       |
|      Description | Text (512 characters) | Detailed description of the part        |
|        Favourite | True/False            | Whether the part is a favourite         |
|           Length | Numeric               | Length of the part                      |
| Publication Year | Numeric               | Publication year of the part            |
|            Image | Image                 | Cover image or thumbnail of the part    |
|             Tags | List of Tags          | Categories the part is contained within |

If a tag for the whole media has a set value (not neutral), all media parts on this media will have the same value for that tag. Media parts can only redefine the neutral tags of the parent media.

#### 5.1.4 Tag Attributes

Each tag that is set for a medium or a medium part can have one of the follow values:

- neutral: no data inserted for the category
- positive: the media or part is contained within the category
- negative: the media or part is not contained within the category

Configured tags contain the following data:

|  Attribute | Data Type             | Explanation                     |
| ---------: | :-------------------- | :------------------------------ |
|         Id | Numeric               | Identification of the tag       |
| Catalog Id | Numeric               | Reference to the parent catalog |
|      Title | Text (128 characters) | Title of the tag                |

#### 5.1.5 Playlist Attributes

Playlists can contain several parts.

Playlists contain the following data:

|  Attribute | Data Type             | Explanation                                  |
| ---------: | :-------------------- | :------------------------------------------- |
|         Id | Numeric               | Identification of the playlist               |
| Catalog Id | Numeric               | Reference to the parent catalog              |
|      Title | Text (128 characters) | Title of the playlist                        |
|      Parts | List of Parts         | Parts that are contained within the playlist |

#### 5.1.6 Settings

Global settings that are independent from catalog contents are stored in a settings table.

Each setting contains the following data:

| Attribute | Data Type             | Explanation                   |
| --------: | :-------------------- | :---------------------------- |
|       Key | Text (512 characters) | Identification of the setting |
|     Value | Text (512 characters) | Content of the setting        |

By default, the application contains the following settings:

|                            Key | Data Type             | Explanation                                                        | Default                                                                |
| -----------------------------: | :-------------------- | :----------------------------------------------------------------- | :--------------------------------------------------------------------- |
|             RESULT_LIST_LENGTH | Numeric               | Length of the pages of any result list within the application      | 20                                                                     |
|     VISIBILITY_PLAYLIST_EDITOR | True/False            | Whether the playlist editor will be shown in the overview menu     | True                                                                   |
|    VISIBILITY_TITLE_OF_THE_DAY | True/False            | Whether the title of the day will be shown in the overview menu    | True                                                                   |
| VISIBILITY_STATISTICS_OVERVIEW | True/False            | Whether the statistics overview will be shown in the overview menu | True                                                                   |
|                    BACKUP_PATH | Text (512 characters) | Path to the backup folder in which the backup files will be stored | A folder named `MediaManager` within the documents of the current user |
|                 BACKUP_ENABLED | True/False            | Whether an automatic backup will be created                        | True                                                                   |
|             CURRENT_CATALOG_ID | Numeric               | Reference to the currently active catalog                          | -                                                                      |

### 5.2 Algorithms

#### 5.2.1 Search Parameters

| Parameter               | Explanation                                                                                                                |
| :---------------------- | :------------------------------------------------------------------------------------------------------------------------- |
| Search Text             | String to use for the applied search                                                                                       |
| Tags                    | Tags to use for the applied search                                                                                         |
| Exact Mode              | In exact mode, the ticked tags must match exactly; otherwise, the ticked tags cannot have the opposite value               |
| Search Description Mode | Whether the text should also be searched within the descriptions; otherwise only the media and part titles will be scanned |
| Favourite Only Mode     | Filter out all parts that are not marked as favourite                                                                      |
| Result List Type        | Whether to show the results as a list of media or as a list of parts                                                       |

Default search parameters: By default, the search text is empty and no tag is selected. Furthermore, no mode will be active and the result list will show the media.

#### 5.2.2 Export and Automatic Backup

The user can export a selected catalog manually by using the export button in the catalog menu. For any export, the default file name will be the name of the exported catalog.

Furthermore, an automatic backup will be performed in the following two cases:

- The application is started whilst a catalog is already active. In this case the currently opened catalog is saved.
- Another catalog is opened via the catalog menu. In this case the previously and newly opened catalog will both be saved.

This backup only is performed, if the corresponding setting is not disabled. If enabled, the backup files would be stored into the configured backup folder. Each file is then named after the exported catalog and a timestamp of the export.

#### 5.2.3 Analyze Results

The analyze menu checks the currently active catalog for inconsistencies. Those include the following:

|                Analysis mode | Explanation                                                                                                  |
| ---------------------------: | :----------------------------------------------------------------------------------------------------------- |
|                 Empty medium | The medium does not contain any parts.                                                                       |
|               Doubled medium | Several media share the exact same title.                                                                    |
|           Missing medium tag | All parts of a medium share the same non-neutral tag value but the value is not globally set for the medium. |
|      Missing media attribute | A given attribute (selectable within a dropdown) is not set for the medium.                                  |
| Missing media part attribute | A given attribute (selectable within a dropdown) is not set for the media part.                              |

## 6 Test Cases

### 6.1 Showing Help

|         **6.1.1** | **Opening the help menu and navigating through topics**                                                                                                                                                                                                                                            |
| ----------------: | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Precondition:** |                                                                                                                                                                                                                                                                                                    |
|      **Actions:** | 1. Click the "Help" button in any menu<br/>2. Select a topic on the lefthand side<br/>3. Navigate through the menu by either using the arrow buttons or arrow keys                                                                                                                                 |
|       **Result:** | 1. The help menu is opened<br/>2. The contents of the topic are shown on the righthand side and if the topic contains several pages, the arrow buttons are enabled<br/>3. The contents of the topic and the selected page are shown in the righthand side and the page counter updates accordingly |

### 6.2 Editing Catalogs

|         **6.2.1** | **Adding and editing a catalog**                                                                                                                                             |
| ----------------: | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Precondition:** |                                                                                                                                                                              |
|      **Actions:** | 1. Click the "Add Catalog" button<br/>2. Insert and confirm data<br/>3. Select the catalog in the list<br/>4. Click the "Edit Catalog" button<br/>5. Change and confirm data |
|       **Result:** | 1. An edit dialog is opened<br/>2. The catalog is created<br/>3. The "Edit Catalog" button is available<br/>4. An edit dialog is opened<br/>5. The catalog is updated        |

|         **6.2.2** | **Exporting, deleting and importing a catalog**                                                                                                                                                              |
| ----------------: | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Precondition:** | at least one catalog exists                                                                                                                                                                                  |
|      **Actions:** | 1. Select a catalog<br/>2. Click the "Export Catalog" button and confirm the export<br/>3. Delete the catalog and confirm the deletion<br/>4. Click the "Import Catalog" button and select the exported file |
|       **Result:** | 1. The "Export Catalog" button is available<br/>2. The catalog is exported to the selected file<br/>3. The catalog is deleted<br/>4. The catalog is created and contains the data from before the deletion   |

### 6.3 Editing and Viewing Media and Media Parts

|         **6.3.1** | **Adding a new medium with parts**                                                                                                        |
| ----------------: | :---------------------------------------------------------------------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the overview menu                                                                                                  |
|      **Actions:** | 1. Click the "Add Medium" button<br/>2. Insert data for the medium<br/>3. Add parts and insert data for the parts<br/>4. Save the changes |
|       **Result:** | The medium is created and now shown in viewing mode                                                                                       |

|         **6.3.2** | **Opening and editing an existing medium**                                                     |
| ----------------: | :--------------------------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the overview menu that contains at least one medium with parts          |
|      **Actions:** | 1. Double click an item in the search result list<br/>2. Click the "Edit Medium" button        |
|       **Result:** | 1. The medium is opened in viewing mode in the edit menu<br/>2. The menu switches to edit mode |

### 6.4 Editing Tags

|         **6.4.1** | **Adding a tag**                                                            |
| ----------------: | :-------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the overview menu                                    |
|      **Actions:** | 1. Click the "Add Tag" button<br/>2. Insert a title and confirm the changes |
|       **Result:** | 1. An edit dialog is opened<br/>2. The tag is created                       |

|         **6.4.2** | **Editing and deleting a tag**                                                                                                                                                       |
| ----------------: | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the tag menu that contains at least one tag                                                                                                                   |
|      **Actions:** | 1. Select a tag<br/>2. Click the "Edit Tag" button<br/>3. Change the title and confirm the changes<br/>4. Select a tag<br/>5. Click the "Delete Tag" button and confirm the deletion |
|       **Result:** | 1. The "Edit Tag" button is available<br/>2. An edit dialog is opened<br/>3. The tag is updated<br/>4. The "Delete Tag" button is available<br/>5. The tag is deleted                |

|         **6.4.3** | **Updating tag values**                                                                                     |
| ----------------: | :---------------------------------------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the tag menu that contains at least one tag and a medium                             |
|      **Actions:** | 1. Select a tag<br/>2. Select a medium<br/>3. Change some tag values and save the changes                   |
|       **Result:** | 1. A list of media is available<br/>2. The media and part tag values are shown<br/>3. The changes are saved |

### 6.5 Searching Media and Media Parts

|         **6.5.1** | **Using the text search including descriptions**                                                                                                         |
| ----------------: | :------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the overview menu that contains several media with parts                                                                          |
|      **Actions:** | 1. Insert a text into the search input and press enter<br/>2. Enable description mode                                                                    |
|       **Result:** | 1. Only results are shown whose medium or part title contains the given text<br/>2. Further results are shown whose descriptions contains the given text |

|         **6.5.2** | **Using the tag search including exact mode**                                                                                         |
| ----------------: | :------------------------------------------------------------------------------------------------------------------------------------ |
| **Precondition:** | a catalog is opened in the overview menu that contains several media with parts                                                       |
|      **Actions:** | 1. Disable exact mode and select a tag<br/>2. Enable exact mode                                                                       |
|       **Result:** | 1. Only results are shown that do not set the opposite tag value<br/>2. Only results are shown that have set the exact same tag value |

|         **6.5.3** | **Searching for favourites**                                                    |
| ----------------: | :------------------------------------------------------------------------------ |
| **Precondition:** | a catalog is opened in the overview menu that contains several media with parts |
|      **Actions:** | 1. Enable the favourites mode                                                   |
|       **Result:** | Only favourites are shown in the search result                                  |

### 6.6 Changing Settings

|         **6.6.1** | **Changing backup settings**                                                                                                                             |
| ----------------: | :------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Precondition:** | several catalogs exist                                                                                                                                   |
|      **Actions:** | 1. Change the backup path<br/>2. If disabled, enable the automatic backup<br/>3. Save the settings<br/>4. Select a different catalog in the catalog menu |
|       **Result:** | The formally active catalog and the newly active catalog are exported to the selected folder                                                             |

|         **6.6.2** | **Showing the playlist editor**                                                                                |
| ----------------: | :------------------------------------------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the overview menu                                                                       |
|      **Actions:** | 1. Enable or disable the playlist editor setting<br/>2. Save the settings<br/>3. Navigate to the overview menu |
|       **Result:** | If the setting is enabled, the playlist editor is available                                                    |

|         **6.6.3** | **Showing the title of the day in different modes**                                                                                                                                                                    |
| ----------------: | :--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the overview menu                                                                                                                                                                               |
|      **Actions:** | 1. Enable or disable the title of the day setting<br/>2. Save the settings<br/>3. Navigate to the overview menu<br/>4. Change the mode of the title of the day within the settings to either show media or media parts |
|       **Result:** | If the setting is enabled, the title of the day is available<br/>According to the settings either a medium or a media part is shown                                                                                    |

|         **6.6.4** | **Showing the statistics**                                                                                |
| ----------------: | :-------------------------------------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the overview menu                                                                  |
|      **Actions:** | 1. Enable or disable the statistics setting<br/>2. Save the settings<br/>3. Navigate to the overview menu |
|       **Result:** | If the setting is enabled, the statistics are available                                                   |

|         **6.6.5** | **Enabling and disabling deletion confirmations**                                                                                             |
| ----------------: | :-------------------------------------------------------------------------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the overview menu that contains at least one medium, part, tag and playlist                                            |
|      **Actions:** | 1. Enable or disable some deletion confirmations<br/>2. Save the settings<br/>3. Check the deletion of media, media parts, tags and playlists |
|       **Result:** | According to the settings, a deletion confirmation is shown or skipped                                                                        |

### 6.7 Analyzing Data

|         **6.7.1** | **Finding and editing empty media**                                   |
| ----------------: | :-------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the analyze menu that contains an empty medium |
|      **Actions:** | 1. Select the "Empty medium" mode within the analyze menu             |
|       **Result:** | A list of empty media is shown                                        |

|         **6.7.2** | **Finding and merging doubled media**                                                                                                                                                |
| ----------------: | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the analyze menu that contains two media with the exact same title                                                                                            |
|      **Actions:** | 1. Select the "Doubled medium" mode within the analyze menu<br/>2. Click the "Edit" button<br/>3. Select media, media information and media parts to merge<br/>4. Submit the preview |
|       **Result:** | 1. A list of doubled media is shown<br/>2. The merge menu is opened<br/>3. A preview is generated<br/>4. The selected media are merged into a single medium as shown in the preview  |

|         **6.7.3** | **Finding and editing media with missing tags**                                                                     |
| ----------------: | :------------------------------------------------------------------------------------------------------------------ |
| **Precondition:** | a catalog is opened in the analyze menu that contains a medium with no tags and a part with all tags set to a value |
|      **Actions:** | 1. Select the "Missing medium tag" mode within the analyze menu                                                     |
|       **Result:** | A list of media with missing media tags is shown                                                                    |

|         **6.7.4** | **Finding and editing media and media parts with missing attributes**                                                                      |
| ----------------: | :----------------------------------------------------------------------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the analyze menu that contains media and parts with partially empty attributes                                      |
|      **Actions:** | 1. Select the "Missing media attribute" or "Missing media part attribute" mode within the analyze menu<br/>2. Select an attribute to check |
|       **Result:** | A list of media or media parts with missing values for the given attribute is shown                                                        |

### 6.8 Editing Playlists

|         **6.8.1** | **Creating a playlist**                                                                                                                   |
| ----------------: | :---------------------------------------------------------------------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the overview menu and the setting for showing playlists is enabled                                                 |
|      **Actions:** | 1. Click the "Add Playlist" button<br/>2. Insert a title and optionally select a tag and playlist length<br/>3. Click the "Create" button |
|       **Result:** | The playlist is created and if selected it is filled with parts matching the selected tag                                                 |

|         **6.8.2** | **Adding parts to and removing parts from a playlist**                                                                                                                                       |
| ----------------: | :------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the overview menu and the setting for showing playlists is enabled; a playlist exists; parts exist within the catalog                                                 |
|      **Actions:** | 1. Select an item in the search result list<br/>2. Right click the search result list and select the option to add the item to a playlist<br/>3. Select a playlist and confirm the selection |
|       **Result:** | The selected parts are added to the chosen playlist and can be removed using the "X" button                                                                                                  |

|         **6.8.3** | **Deleting a playlist**                                                                                      |
| ----------------: | :----------------------------------------------------------------------------------------------------------- |
| **Precondition:** | a catalog is opened in the overview menu and the setting for showing playlists is enabled; a playlist exists |
|      **Actions:** | 1. Select a playlist<br/>2. Click the "Delete Playlist" button and confirm the deletion                      |
|       **Result:** | 1. The "Delete Playlist" button is enabled<br/>2. The playlist is deleted                                    |

## 7 Appendix

The illustrations were created using either `draw.io` or a running instance of the application.
