# CSV Translation Packer

I've just used a pretty nice translation service, but it is extracts results as a `.zip` file with several translations as `.csv` files.  
My target translation should look like one combined `.csv` file with a bunch of different columns.  
Transforming them manually everytime would be an incredibly tedious chore, so, that's where this little app comes in handy.

## Logic flow:

1. Read `.zip` file path from first command line argument.
2. Extract languages from file names like `*.[lang].csv`.
3. Extract each file and parse content. Here's the input tables (be aware of empty column):
<div align="center">
  
|[KEY]||[VALUE]|
|---|---|---|
|[KEY]||[VALUE]|

</div>

4. Combine all extracted values into one big translation table. Here's the output table:
<div align="center">
  
|Key|Language 1|...|Language N|
|---|---|---|---|
|[KEY]|[VALUE]|...|[VALUE]|

</div>

5. Save this file in the same directory, where command was queiried.

## Notes
Pretty sure nobody will be viewing this repo, but if you do, feel free to do whatever you want with this code, no licenses whatsoever.  
Just created this to help me parse translation files for some personal project and uploaded to not lost it.

<b>(c) tshmofen</b>
