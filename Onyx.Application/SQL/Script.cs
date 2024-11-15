namespace Onyx.Application.SQL
{
    internal class Script
    {
        /*

        Given corresponding database tables TravelAgent and Observation, both with a TravelAgent field being the primary and foreign key, respectively:


        CREATE TABLE TravelAgents (
            TravelAgentID SERIAL PRIMARY KEY
        );

        CREATE TABLE Observations (
        ID SERIAL PRIMARY KEY,
        TravelAgentID INT,
        CONSTRAINT fk_travelagent FOREIGN KEY (TravelAgentID) 
        REFERENCES TravelAgents (TravelAgentID) 
        ON DELETE CASCADE -- Delete orders if the related customer is deleted
        ON UPDATE CASCADE -- Update orders if the related customer's ID changes);


        c) Write a SQL query that finds all travel agents that does not have any observations.


        SELECT travelagents.travelagentid
        FROM travelagents
        left join observations
        ON travelagents.travelagentid = observations.travelagentid
        WHERE observations.travelagentid IS NULL

        // Other solution
        SELECT travelagentid
        FROM travelagents
        WHERE NOT EXISTS (
        SELECT 1
        FROM observations
        WHERE observations.travelagentid = travelagents.travelagentid
        );


        d) Write a SQL query that finds all travel agents that have more than two observations.

        SELECT travelagents.travelagentid
        FROM travelagents
        LEFT JOIN observations
        ON travelagents.travelagentid = observations.travelagentid
        GROUP BY travelagents.travelagentid
        HAVING COUNT(travelagents.travelagentid) > 2;

         */
    }
}
