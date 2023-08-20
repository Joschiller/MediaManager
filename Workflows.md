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
7. Create Release in Repository
   1. `git checkout -b X.X.X-rc`
   2. push new branch
   3. Github > Tags > Create a new Release
      - Tag: "vX.X.X" -> set to create "on publish"
      - Target branch: "X.X.X-rc"
      - Title: "vX.X.X"
      - Description: Copy from Changelog
      - Files: add executable or similar binary file

# Update against template

> Run once: `git remote add template https://github.com/Joschiller/Project_Template`

1. `git fetch --all`
2. `git merge template/main --allow-unrelated-histories`
