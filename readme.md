# CSV Translation Packer (.NET 8.0)

I've just used a pretty nice translation service, but it is exports results as a `.zip` file with several translations as `.csv` files.
My target translation should look like one combined `.csv` file with a bunch of different columns.  
Transforming them manually everytime would be an incredibly tedious chore, so, that's where this little app comes in handy.

## Logic flow:

1. First time app is run, it is gonna create settings file `settings.json` (Almost all settings are pretty self-descriptive, only note that `InputFileSearch` is using wildcards to find file and `KeyColumnName` is column name for output file).
2. The second time app is run, it is gonna start parsing found `.zip` file.
3. Then it is starting parsing languages from file names in archive like `*.[lang].csv`.
4. After that, it is extracting each file and parsing content. Here's the expected input table, make sure to specify correct column indexes in the settings:
<div align="center">
  
|[KEY]|...|[VALUE]|
|---|---|---|
|[KEY]|...|[VALUE]|

</div>

5. All parsed files than are combined into one big translation table. Here's the output format, where `Key` column name can be specified in settings:
<div align="center">
  
|Key|Language 1|...|Language N|
|---|---|---|---|
|[KEY]|[VALUE]|...|[VALUE]|

</div>

6. And finally the file is saved in the directory, where app was executed from. Output file name can be set up through settings.

## Notes
Pretty sure nobody will be viewing this repo, but if you do, feel free to do whatever you want with this code, no licenses whatsoever.  
Just created this lil app to help me in my personal project and uploaded here to not lose it.

<b>(c) tshmofen</b>
