import { apiCall, API_URL } from './httpClient.js';

export function setupAuthListeners() {
    const loginForm = document.getElementById("login-form");
    const signupForm = document.getElementById("signup-form");
    const logoutButton = document.getElementById("logout-btn");

    if (loginForm) {
        loginForm.addEventListener("submit", async (e) => {
            e.preventDefault();

            try {
                const response = await fetch(`${API_URL}/auth/login`, {
                    method: "POST",
                    headers: { "Content-Type": "application/json" },
                    body: JSON.stringify({
                        username: document.getElementById("username").value,
                        password: document.getElementById("password").value
                    })
                });

                if (response.ok) {
                    window.location.href = "../index.html";
                }
                else {
                    const err = await response.json();
                    document.getElementById("login-error").innerText = err.error;
                }
            }
            catch (error) {
                console.error("Login error: ", error);
                throw error;
            }
        });
    }

    if (signupForm) {
        signupForm.addEventListener("submit", async (e) => {
            e.preventDefault();

            try {
                const response = await fetch(`${API_URL}/auth/register`, {
                    method: "POST",
                    headers: {"Content-Type": "application/json"},
                    body: JSON.stringify({
                        username: document.getElementById("username").value,
                        email: document.getElementById("email").value,
                        password: document.getElementById("password").value
                    })
                });

                if (response.ok) {
                    window.location.href = "../pages/login.html";
                } else {
                    const err = await response.json();
                    document.getElementById("signup-error").innerText = err.error;
                }
            } catch (error) {
                console.error("Signup error: ", error);
                throw error;
            }
        });
    }

    if (logoutButton) {
        logoutButton.addEventListener("click", async (e) => {
            e.preventDefault();

            try {
                await apiCall("/auth/logout", "POST");

                window.location.href = "../pages/login.html";
            }
            catch (error) {
                console.error("Logout error: ", error);
                throw error;
            }
        })
    }
}