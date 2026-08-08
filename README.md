This is a personal project that I developed to analyze the recorded data from a [TopDon Phoenix Max](https://www.topdon.us/collections/diagnostic-tools/products/phoenix-max) scan tool.

An example log file is included in the [assets](assets/) folder.

**Please Note:**

I have currently only provided the ability to reliably open the ECM and TCM datalogs acquired from a Volvo P2 XC90. Due to the undocumented nature of this log file architecture, you would need to understand the layout in order to modify my code. As of 08/08/2026, I have not documented any of the application logic.

If you experience a crash, please send me the log file and I will make the required changes to open it.

I am still wrestling with the scope of this application and have made no commitments to the current design of the UI.  I am currently refactoring the underlying data handling classes to allow for conversion from the default SAE units.  Please bear with me and contact me if you have any questions or suggestions.

This project was made infinitely better with the incredible plotting library: [ScottPlot](https://github.com/scottplot/scottplot)  written by [Scott Harden](https://swharden.com/about/) Thank you @ScottPlot!
