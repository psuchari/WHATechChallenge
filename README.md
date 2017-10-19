# WHATechChallenge
William Hill Technical Assessment

Solution is a ASP.NET Web Form
The form contains 2 controls that accepts csv input files.
The settled bets csv file must contain the columns Customer, Stake and Win.
The unsetled bets csv file must contain the columns Customer, Stake and To Win.
Once both csv files have been selected, the analyse button will become enabled. 
Clicking the analyse button will generate 2 tables. 
The first table is a summary of the settled bets csv file showing each customer's average bet, number of bets, number of wins and the win percentage.
The second table is a visual display of the unsettled bets. 
Risky Customers, defined as those with a win rate of 60% or more will have the row background colour highlighted yellow.
Unsual Bets, defined as bets greater than 10 times their average bets will have the text colour changed to orange.
Highly Unsual Bets, defined as bets greater than 30 times their average bets will have the text colour changed to red.
Bets with payout greater than 1000 will have the text bolded.

Since the app currently does not handle any transactions upon customer nor does it perform any transactions on bets, it is uneccessary to model these in the solution.
