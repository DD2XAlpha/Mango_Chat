// Fetch GET Method: Sends parameters as a query string and returns the response data.
async function FetchGet(url, params = {}) {
    try {
        // Convert params object to query string
        const queryString = new URLSearchParams(params).toString();
        const fullUrl = queryString ? `${url}?${queryString}` : url;

        // Perform the GET request
        const response = await fetch(fullUrl, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        // Check if the response is OK
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        // Parse the JSON response
        const data = await response.json();
        return data;

    } catch (error) {
        console.error('Error in FetchGet:', error.message);
        throw error; // Re-throw the error for the caller to handle
    }
}

// Fetch POST Method: Sends JSON payload and returns the response data.
async function FetchPost(url, body = {}) {
    try {
        // Perform the POST request
        const response = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body) // Send the body as JSON
        });

        // Check if the response is OK
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        // Parse the JSON response
        const data = await response.json();
        return data;

    } catch (error) {
        console.error('Error in FetchPost:', error.message);
        throw error; // Re-throw the error for the caller to handle
    }
}

// Export the functions if using modules
//export { FetchGet, FetchPost };
