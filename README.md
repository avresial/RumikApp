#   Hi, this is **RumikApp**

At the begining I'd like to mention that it's my first personal project made at that scale. Repo consists of 3 main directories:

1. RumikApp - Main app
2. RumikApp.Tests - Unit tests
3. ApprovalToolForRumikApp - Side app allowing administrator to move data between main and additional databases.

## Simply - **RumikApp** is the app where you go to if you are looking for a Rum

App allows user to:

-   Find new rum depending on his current needs,
-   Browse external and internal database,
-   Save his own entries.

# Contents page

1.  [Technologies](#Technologies)
2.  [App presentation](#AppPresentation)


#   Technologies <a name="Technologies"></a>

## Generall 

Whole app is based on .NET Framework 4.7.2 and C#.

Below I'm listing couple of technologies that I learned during this project:

-   Linq
-   MVVM
-   Autofac 
-   Xunit and Moq
-   Handling MySQL database
-   Asynchronous programming

At this point app supports only Polish language.

## Future plans

I'm planning to implement Entity framework.

#   App presentation <a name="AppPresentation"></a>

Below you will find couple of screens from RumikApp itself

![Main window](ReadmeImages/Fig1.png)
**Fig.1** Startup page

![Database view](ReadmeImages/Fig2.png)
**Fig.2** Database view

![Poll](ReadmeImages/Fig3.png)
**Fig.3** Poll

![My place](ReadmeImages/Fig4.png)
**Fig.4** My rum Cabinet


![Random](ReadmeImages/Fig5.png)
**Fig.5** Random entry


![Add new](ReadmeImages/Fig6.png)
**Fig.6** Add new entry