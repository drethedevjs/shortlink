# API Instructions
## Interact with the API
1. Clone web api into your directory.
2. Since this is only a web api, you will need a way to hit the endpoints. My preferred method is to use Postman: [Download](https://www.postman.com/downloads/).
3. Once downloaded create three tabs and add the following links in each respective one.
    - POST https://localhost:5001/api/shortlink/encode?longLink=http://example.com
    - GET https://localhost:5001/api/shortlink/all
    - GET https://localhost:5001/api/shortlink/decode?shortenedLink=http://short.est/uvFxEfsfdQ

   Be sure to change the REST method to match each link.

4. Open your terminal and navigate to the *shortlink* directory inside the cloned repo.
5. Type the following below pressing *Enter* after each line. The last command will start the web api for you to begin using it.
    
    ```
    dotnet restore
    dotnet build
    dotnet run
    ```
6. Navigate back to Postman (or whatever equivalent program of your choice) and run the commands in step 3. 
    - The first tab (POST) will add links to the database. After each call, you'll recieve a json object in return signifying that the program is working and is storing data into the in-memory database.
    - The second tab (GET) will show all the links that you have added to the database. Pick a short link (that is, the `"shortenedLink"` property) and copy it (the value).
    - The third tab is where you can retreive links that have already been added. Ensuring that the "Params" tab under the url field is selected, paste the copied link into the *Value* column and write the word `shortenedLink` into the *Key* column. Once you have done this, click the *Send* button. You should see the link pair associated with that shortened link return as a json object.

## Run Tests
Open your IDE of choice and open the *ShortLinkAPI.Tests* folder. Once inside, use your IDE's testing panel to test the endpoints.

### Don't know how to do that?
Then feel free to use the IDE that I use: [VS Code](https://code.visualstudio.com/Download)

1. Open VS Code.
2. Open the *ShortLinkAPI.Tests* folder but clicking *File > Open* then navigating that folder in the cloned repo.
3. Select the *ShortLinkTests.cs* file from the sidebar.
4. Right above the class name--above where it says *public class ShortLinkTest : InMemoryDbTests*--click the words `Run All Tests`. You should see a terminal pane pop-up on the bottom of the window showing the results of the tests.
