import  {apiCall } from "./httpClient.js";

export function setupStatsListeners() {
    const monthPicker = document.getElementById("stats-month-picker");

    if (monthPicker) {
        const now = new Date();
        const year = now.getFullYear();
        const month = String(now.getMonth() + 1).padStart(2, "0");

        monthPicker.value = `${year}-${month}`;

        monthPicker.addEventListener("change", loadStats);
    }
}

export async function loadStats() {
    try {
        const monthPicker = document.getElementById("stats-month-picker");
        let month, year;

        if (monthPicker && monthPicker.value) {
            const parts = monthPicker.value.split("-");
            year = parseInt(parts[0], 10);
            month = parseInt(parts[1], 10);
        }
        else {
            const now = new Date();
            month = now.getMonth() + 1;
            year = now.getFullYear();
        }

        const monthlyData = await apiCall(`/stats/monthly?Month=${month}&&Year=${year}`);
        document.getElementById("monthly-total").textContent = `${Number(monthlyData).toFixed(2)} ₴`;

        const categoriesStats = await apiCall("/stats/categories");
        const list = document.getElementById("stats-categories-list");
        list.innerHTML = "";

        if (Array.isArray(categoriesStats)) {
            categoriesStats.forEach(stat => {
               const li = document.createElement("li");
               li.innerHTML = `
                    <span>${stat.name}</span>
                    <strong>${Number(stat.amount).toFixed(2)} ₴</strong>
               `;
               list.appendChild(li);
            });
        }

    }
    catch (error) {
        console.error("Stats loading error: ", error);
    }
}