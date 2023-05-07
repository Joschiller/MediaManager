# Release

1. Check API
   - Configuration in `CatalogExportThread` and `CatalogImportThread` code
   - Description in [specification file](XML-API.md) including all changelog and migration information
   - also check the correct version number in respect to the performed changes
2. Check Workflow documentation
3. Update [Application Description](./Application%20Description.md) and perform a check of the content for any erros (e.g. using MS Word)
4. Update [Changelog](./Changelog.md)
   - document new features
   - document open issues that have a direct impact regarding the functionality
     > Add a "> Solved with Version x.x.x" mark at any open issue when it is solved in a later version.
   - document solved bugs
5. Make testbuild in a copied folder and test functionality
   - check added functionality for basic workflow
   - check validation of wrong or missing inputs
   - check if all validation messages are shown for missing inputs
6. Simplify code
   - remove unnecessary imports
   - check autocompletion hints
7. FURTHER STEPS ...
8. Commit changes: "release: Version x.x.x"
9. Add tag to commit: "Version x.x.x"

# File Structure

## File Regions

```c#
#region Events
// handler, delegates
#endregion
#region Properties
// bindings, that are also used as getter/setter
#endregion
#region Bindings
// internal bindings
#endregion
#region Setup
// private state attributes that do not belong to bindings
// constants
// constructors
// LanguageProvider
// setup/reload-methods
#endregion
#region Getter/Setter
// public methods
#endregion
#region Handler
// internal event handlers and corresponding methods
#endregion
```

## Constructors

```c#
InitializeComponent();
DataContext = this;
// ... register needed handlers
RegisterAtLanguageProvider(); /* or */ LoadTexts();
// ... further setup
```

# Application Changes

## XML Versioning Default Schema Template

- <b>`SchemaName`</b>
  - <b>Description:</b> ...
  - <b>Type:</b> `object`
  - <b>Properties:</b>
    - `...`
      - <b>Type:</b> `...`
      - <b>Description:</b> ...
      - <b>Default:</b> ...
      - <b>Null Replacement:</b> ...
      - <b>Min Value:</b> ...
      - <b>Max Value:</b> ...
      - <b>Max Length:</b> ...
      - <b>Pattern:</b> ...

## Modifying the XML Export

When modifying the XML export always complete these steps:

- Modify the versioning accordingly. If a change has already been done since the last release, only increase the version if needed. (See `DataConnector.CurrentExportVersion`)
- Check the downwards compatibility of the export files and change the attributes accordingly. (See `DataConnector.MinimumImportVersion`)
- Add a new section (or update the newest section for the upcoming release) in the xml versioning description.
  - Include a complete copy of the older version description here with an additional change section on top, to describe the last changes.
  - Also include a migration guide to update older file versions to the new version, in case the application needs to have more data provided that cannot be optional. - Otherwise always try to make new fields optional!
