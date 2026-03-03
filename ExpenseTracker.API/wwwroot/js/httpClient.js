export const API_URL = "https://localhost:7195/api";

export async function apiCall(endpoint, method = "GET", body = null) {
    const options = {
        method,
        headers: { "Content-Type": "application/json" },
        credentials: "include"
    };

    if (body)
        options.body = JSON.stringify(body);

    const response = await fetch(`${API_URL}${endpoint}`, options);

    if (response.status === 401) {
        window.location.href = "../pages/login.html";
        return null;
    }

    if (!response.ok) {
        const errorText = await response.text();
        let errorMessage = "Unknown error occurred";

        if (errorText) {
            try {
                const errorObj = JSON.parse(errorText);

                if (errorObj.error) errorMessage = errorObj.error;
                else if (errorObj.errors) errorMessage = Object.values(errorObj.errors).flat().join('\n');
                else if (errorObj.title) errorMessage = errorObj.title;
                else errorMessage = errorText;

            } catch (error) {
                errorMessage = errorText;
            }
        }

        const errorContainer = document.getElementById("main-error");
        if (errorContainer) {
            errorContainer.innerText = errorMessage;
        }

        throw new Error(errorMessage);
    }

    if (response.status === 204) return null;

    const text = await response.text();

    return text ? JSON.parse(text) : null;
}