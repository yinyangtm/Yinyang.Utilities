# Release Notes


## v1.0.2 (04/29/2025)

* **New Features:**

  - **StrConvHelper Enhancements**: Added half-width and full-width character conversion functionality.


## v1.0.1 (11/10/2024)

* **New Features:**

  - **ExtensionStringClass**: Added a utility class to enhance string manipulation and file handling capabilities. This class includes the following methods:
    - **RemoveLineBreaks**: Removes all line breaks (both `\r` and `\n`) from a string, returning a version of the string without any line breaks.
    - **ToLinuxLineEndings**: Converts Windows-style line endings (`\r\n`) to Linux-style line endings (`\n`) within the specified string.
    - **ToWindowsLineEndings**: Converts Linux-style line endings (`\n`) to Windows-style line endings (`\r\n`) within the specified string.
    - **WriteToFile**: Writes the string content to a specified file, with support for encoding. Overwrites the file if it already exists. Users can specify the encoding, including `UTF-8`, `UTF-16`, and `EUC-JP`.
    - **AppendToFile**: Appends the string content to a specified file, with support for encoding. Creates the file if it does not exist. This method also supports encodings such as `UTF-8`, `UTF-16`, and `EUC-JP`.


## v1.0.0 (06/26/2023)

* Released.
