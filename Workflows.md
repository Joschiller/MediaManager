# Release

1. Check Workflow documentation
2. Update [Application Description](./Application%20Description.md) and perform a check of the content for any erros (e.g. using MS Word)
3. Update [Changelog](./Changelog.md)
   - document new features
   - document open issues that have a direct impact regarding the functionality
     > Add a "> Solved with Version x.x.x" mark at any open issue when it is solved in a later version.
   - document solved bugs
4. Make testbuild in a copied folder and test functionality
   - check added functionality for basic workflow
   - check validation of wrong or missing inputs
   - check if all validation messages are shown for missing inputs
5. Simplify code
   - remove unnecessary imports
   - check autocompletion hints
6. FURTHER STEPS ...
7. Commit changes: "release: Version x.x.x"
8. Add tag to commit: "Version x.x.x"
