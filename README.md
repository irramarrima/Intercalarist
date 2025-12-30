# Intercalarist
A free tool for creating custom solar calendars.

<p align="center">
<img width="40%" alt="Sample 1" src="https://github.com/user-attachments/assets/d2ba6b26-6db9-4815-8fd7-23d678bc9078"/>
<img width="40%" alt="Sample 2" src="https://github.com/user-attachments/assets/9ad4aa1c-2e53-4dd8-a658-d65f92e192a8" />
</p>

WHAT CAN I DO WITH THIS TOOL?

- Create your own solar calendars, distributing the days of the year (365 plus a leap day) in whichever
configuration you need.
- Add notes for each week of the year. The last relevant note will be shown automatically when a calendar
is opened.

WHAT CAN I NOT DO?

- Count cycles.
- Have more or less than 365 days per cycle. (Usually required for lunar and lunisolar calendars.)
- Have weeks with more or less than 7 days.
- Use non-Gregorian rules for leap years.
- Display or create montly grids for printing.

WHY WAS IT DESIGNED THIS WAY?

- Processional sictacurmists need an intuitive way of tracking time in their cyclical practices. Creating custom calendars to help rationalize and internalize the extent and pacing of these practices becomes the easiest solution (for instance, to quickly count weekends/months/periods until the sun reaches or returns to a given position). The widespread Gregorian calendar provides a reliable backbone.
- An integrated notes feature is fundamental as the practice grows more and more complex over the years. This way, details relevant at specific moments of the cycle and lasting for a number of weeks/months/periods are easier to follow.
- Listing dates makes display as reasonable for all configurations, since a custom month/period of 100 days would not be very helpful or appealing in a grid shape.

LEAP DAY MECHANICS

Custom calendar cycles always begin on the specified date regardless of leap years, so that a cycle beginning on Gregorian ordinal 200 in a regular year will begin on ordinal 201 in a leap year.
Cycles beginning between January 1 and February 28 in a leap year will include a leap day. On the other hand, if a cycle starts on or after May 1, the cycle with the leap day will start in the year preceding the Gregorian leap year.

## Distribution and Downloads
The compiled application is available for download in the Releases section of this repository.

* **Latest Release:** [Download the latest release](../../releases)

## Licensing and Copyright
This software is released under the GNU General Public License v3.0.

* **Source Code:** All source files are subject to the terms of the GPL. [View the Licenses](./LICENSE)
* **Graphic Assets:** The application icons and brand assets were created by the author and are included under the same GPL v3.0 license grant.

## Installation and Usage
1. Download the latest binary zip file from the Releases page.
2. Extract the contents to a local directory.
3. Execute `Intercalarist.exe`.

## Building from Source
To build this project locally, you will need:
* Visual Studio 2025 (or later)
* .NET SDK 10.0

Open the solution file (`.slnx`) and select 'Build Solution' from the Build menu.

---
Copyright (C) 2025 Irramárrima.
