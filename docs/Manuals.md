# Media Manager

<div style="background: #141414;" width="100%" height="640px">
  <img style="margin: 40%; padding-right: 64px" src="./styles/app_icon/icon.png" height="128px"/>
</div>

<div style="page-break-after: always;"></div>

- [1 Introduction](#1-introduction)
- [2 Help Menu](#2-help-menu)
- [3 Editing Catalogs](#3-editing-catalogs)
- [4 Overview Menu](#4-overview-menu)
  - [4.1 Adding Media](#41-adding-media)
  - [4.2 Searching Media and Media Parts](#42-searching-media-and-media-parts)
- [5 Editing Media and Media Parts](#5-editing-media-and-media-parts)
- [6 Settings](#6-settings)
- [7 Analyze Menu](#7-analyze-menu)
- [8 Useful Keyboard Shortcuts](#8-useful-keyboard-shortcuts)

## 1 Introduction

The _Media Manager_ application can be used to list different types of media with all their contents. Each media and media part can be categorized using tags as well as other metadata that can be used to search for specific items within the application. The application offers a searching functionality as well as several other support features that enhance the creation of media catalogs.

## 2 Help Menu

The help menu can be reached from all other menus by using the "Help" button in the menu bar. The help menu shows different topics in the lefthand list from which the user can select a desired topic. The user can then navigate through the topics and pages of the topics by using the arrow buttons or the arrow keys.

![Help Menu](../HelpImages/HelpMenu.png)

<div style="page-break-after: always;"></div>

## 3 Editing Catalogs

Within the catalog menu, catalogs can be created, edited or deleted. Furthermore, the active catalog can be selected by double clicking it or catalogs can be exported and imported.

The catalog menu is the first menu that will be opened, if no catalog exists yet. The menu then offers the access to the catalog creation as well as the settings and help menu.

![Catalog Menu](../HelpImages/CatalogMenu.png)

<img style="float: right;" src="./styles/editing/add_catalog.png" height="80px" />

When clicking the "Add Catalog" button in the menu bar, a dialog will be opened to insert a title for the catalog.

![Catalog Menu Dialog](../HelpImages/CatalogMenu_Edit.png)

<div style="page-break-after: always;"></div>

## 4 Overview Menu

The overview menu provides the searching functionality on the righthand side of the menu as well as the access to all other menus. Furthermore, tags can be created by opening the corresponding dialog or submenu.

On the lefthand side of the menu, further controls can be enabled that allow editing playlists, viewing the title of the day or viewing statistics.

![Overview Menu](../HelpImages/OverviewMenu.png)

### 4.1 Adding Media

<img style="float: right;" src="./styles/editing/add_medium.png" height="80px" />

Media can be added by clicking the "Add Medium" button in the menu bar. That way the edit menu will be opened for the creation of a new medium.

### 4.2 Searching Media and Media Parts

The search on the righthand side of the overview menu allows to filter for media and media parts by title, tags and further attributes.

To open an existing medium from the overview menu, an item in a playlist or the search result can be double clicked.

<img src="../HelpImages/OverviewMenu_Search.png" height="350px" />

<div style="page-break-after: always;"></div>

## 5 Editing Media and Media Parts

<img style="float: right;" src="./styles/editing/edit_blue.png" height="80px" />

If a medium is opened in the edit menu, the media and media part information can be edited. Therefore, editing can be started in the menu bar, in case the menu was opened to view a medium. Then the data can be edited and the changes can be saved.

> <span style="color:red"><b>Changes must be shaved explicitly and will be discarded otherwise!</b></span>

Media and parts are each described by a title and can hold additional information such as a description and tags. Parts also can contain further data such as images, a length and a publication year, whilst media can have a location.

If tags are set for the whole medium, these values will be applied for all of their contained parts.

![Edit Menu](../HelpImages/EditMenu.png)
![Edit Menu](../HelpImages/EditMenu_Part.png)

<div style="page-break-after: always;"></div>

## 6 Settings

The settings allow the configuration of the application. This includes the setup of the automatic backup, catalog settings and further global settings such as the language and the available features in the overview menu.

![Edit Menu](../HelpImages/SettingsMenu_Backup.png)
![Edit Menu](../HelpImages/SettingsMenu_Catalog.png)
![Edit Menu](../HelpImages/SettingsMenu_Global.png)

<div style="page-break-after: always;"></div>

## 7 Analyze Menu

The analyze menu provides the access to different inconsistencies that were found within the currently active catalog. This includes empty or doubled media as well as missing attributes for media or their parts.

The found items are shown in a preview and can then be opened in the edit menu by clicking the edit button.

In case of doubled media, the items are not edited in the edit menu. Rather they are opened in a merge menu that allows the combination of the desired media information and parts to create a single combined medium instead of the doubled items.

![Analyze Menu](../HelpImages/AnalyzeMenu.png)
![Merge Menu](../HelpImages/AnalyzeMenu_MergeEdit.png)

## 8 Useful Keyboard Shortcuts

| Menu          | Shortcut                          | Function                                                                        |
| :------------ | :-------------------------------- | :------------------------------------------------------------------------------ |
| -             | F1                                | Show help                                                                       |
| -             | F2                                | Open settings menu                                                              |
| Help Menu     | Esc                               | Close menu                                                                      |
| Help Menu     | Arrow Keys                        | Navigation if the topic list has focus                                          |
| Catalog Menu  | Esc                               | Close menu                                                                      |
| Catalog Menu  | Ctrl + N                          | Add catalog                                                                     |
| Catalog Menu  | Ctrl + I                          | Import catalog                                                                  |
| Catalog Menu  | Ctrl + L                          | Open selected catalog                                                           |
| Catalog Menu  | Ctrl + E                          | Edit selected catalog                                                           |
| Catalog Menu  | Ctrl + O                          | Export selected catalog                                                         |
| Catalog Menu  | Ctrl + D<br/>Del.                 | Delete selected catalog                                                         |
| Overview Menu | Ctrl + N                          | Add medium                                                                      |
| Overview Menu | Ctrl + T                          | Add tag                                                                         |
| Overview Menu | Ctrl + R                          | Reset search                                                                    |
| Overview Menu | Ctrl + Shift + T                  | Open tag menu                                                                   |
| Overview Menu | Ctrl + Shit + C                   | Open catalog menu                                                               |
| Overview Menu | Ctrl + Shit + A                   | Open analyze menu                                                               |
| Overview Menu | Arrow Keys                        | Navigation through search result list if it has focus                           |
| Overview Menu | Ctrl + Enter                      | Open selected medium                                                            |
| Edit Menu     | Esc                               | Close menu                                                                      |
| Edit Menu     | Ctrl + E                          | Edit medium                                                                     |
| Edit Menu     | Ctrl + R                          | Discard changes                                                                 |
| Edit Menu     | Ctrl + S                          | Save medium                                                                     |
| Edit Menu     | Ctrl + N                          | Add a new part                                                                  |
| Edit Menu     | Ctrl + M                          | Show media information                                                          |
| Edit Menu     | &uarr;/&darr; Arrow Keys          | Navigation through media part list if it has focus to show the part information |
| Edit Menu     | Ctrl + F                          | Toggle favourite for currently selected media part                              |
| Edit Menu     | Ctrl + I                          | Select image for currently selected media part                                  |
| Edit Menu     | Ctrl + Shift + I                  | Clear image for currently selected media part                                   |
| Edit Menu     | Ctrl + D<br/>Del.                 | Delete selected part or medium                                                  |
| Tag Menu      | Esc                               | Close menu                                                                      |
| Tag Menu      | Ctrl + N                          | Add tag                                                                         |
| Tag Menu      | Ctrl + E                          | Edit selected tag                                                               |
| Tag Menu      | Ctrl + D<br/>Del.                 | Delete selected tag                                                             |
| Tag Menu      | Ctrl + S                          | Save changed tags                                                               |
| Tag Menu      | Ctrl + R                          | Discard changed tags                                                            |
| Tag Menu      | &uarr;/&darr; Arrow Keys          | Navigation through tag list if it has focus                                     |
| Tag Menu      | Arrow Keys                        | Navigation through media list if it has focus                                   |
| Settings Menu | Esc                               | Close menu                                                                      |
| Settings Menu | Ctrl + R                          | Discard changes                                                                 |
| Settings Menu | Ctrl + S                          | Save settings                                                                   |
| Settings Menu | Ctrl + Tab<br/>Ctrl + Shift + Tab | Change tab                                                                      |
| Analyze Menu  | Esc                               | Close menu                                                                      |
| Analyze Menu  | Ctrl + &uarr;/&darr; Arrow Keys   | Change mode                                                                     |
| Analyze Menu  | Ctrl + E                          | Edit selected element                                                           |
| Analyze Menu  | Arrow Keys                        | Navigation through media list if it has focus                                   |
| Merge Menu    | Esc                               | Close menu                                                                      |

> Furthermore, there exist key combinations for most buttons with textual content.
