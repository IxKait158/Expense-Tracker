import { apiCall } from './httpClient.js';
import { loadStats } from "./stats.js";

export async function handleTransactionSubmit(e) {
    e.preventDefault();
    const amount = parseFloat(document.getElementById("amount").value);
    const date = document.getElementById("date").value;
    const categoryId = document.getElementById("category-select").value;
    const comment = document.getElementById("comment").value;

    try {
        await apiCall("/transactions", 'POST', { amount, date, categoryId, comment });
        document.getElementById('amount').value = '';
        document.getElementById('comment').value = '';
        document.getElementById('category-select').value = '';

        await loadTransactions();
        await loadStats();
    } catch (error) {
        console.error("Transaction creating error: ", error);
    }
}

async function deleteTransaction(id) {
    if (!confirm("Are you sure you want to delete the transaction?")) return;
    try {
        await apiCall(`/transactions/${id}`, 'DELETE');

        await loadTransactions();
        await loadStats();
    } catch (error) {
        console.error("Transaction deleting error: ", error);
    }
}

async function turnIntoTransactionEditMode(li, t) {
    let formattedDateForInput = "";
    if (t.date && !t.date.startsWith("0001")) {
        formattedDateForInput = t.date.substring(0, 10);
    }

    const categoryOptionsHtml = document.getElementById("category-select").innerHTML;

    li.innerHTML = `
        <div class="list-item-header" style="align-items: flex-start;">
            <div class="transaction-edit-form">
                <div class="transaction-edit-row">
                    <input type="number" class="edit-inline-input" id="edit-amount-${t.id}" value="${t.amount}" step="0.01" style="width: 90px;" placeholder="Amount">
                    <input type="date" class="edit-inline-input" id="edit-date-${t.id}" value="${formattedDateForInput}" style="flex: 1; padding: 4px 5px;">
                </div>
                
                <select class="edit-inline-input" id="edit-category-${t.id}" style="padding: 5px; width: 100%;">
                    ${categoryOptionsHtml}
                </select>
                
                <input type="text" class="edit-inline-input" id="edit-comment-${t.id}" value="${t.comment || ''}" placeholder="Comment" style="width: 100%;">
            </div>
            
            <div class="action-buttons-col">
                <button class="save-btn">Save</button>
                <button class="cancel-btn">Cancel</button>
            </div>
        </div>
    `;

    const selectEl = li.querySelector(`#edit-category-${t.id}`);
    if (t.categoryId) selectEl.value = t.categoryId;

    li.querySelector('.save-btn').addEventListener('click', () => {
        saveTransaction({
            id: t.id,
            amount: parseFloat(document.getElementById(`edit-amount-${t.id}`).value) || t.amount,
            date: document.getElementById(`edit-date-${t.id}`).value || t.date,
            categoryId: document.getElementById(`edit-category-${t.id}`).value || t.categoryId,
            comment: document.getElementById(`edit-comment-${t.id}`).value || t.comment
        });
    });

    li.querySelector('.cancel-btn').addEventListener('click', () => loadTransactions());
}

async function saveTransaction(updatedData) {
    if (!updatedData.categoryId) {
        document.getElementById("main-error").innerText = "Please select a category.";
        return;
    }

    try {
        await apiCall(`/transactions`, 'PUT', updatedData);

        await loadTransactions();
        await loadStats();
    } catch (error) {
        console.error("Saving transaction error: ", error);
    }
}

export async function loadTransactions() {
    try {
        const transactions = await apiCall("/transactions");
        const list = document.getElementById("transactions-list");
        list.innerHTML = "";

        transactions.forEach(t => {
            const dateStr = t.date;
            const formattedDate = dateStr
                ? new Date(dateStr).toLocaleDateString('en-GB', {
                    day: '2-digit', month: 'short', year: 'numeric'
                })
                : 'No date';

            const li = document.createElement("li");
            li.innerHTML = `
                <div class="list-item-header">
                    <div class="transaction-details">
                        <div class="transaction-main">
                            <strong></strong>
                            <span class="t-date"></span>
                        </div>
                        <small></small>
                    </div>
                    <div class="action-buttons">
                        <button class="edit-btn" data-id="${t.id}">Edit</button>
                        <button class="delete-btn" data-id="${t.id}">Delete</button>
                    </div>
                </div>
            `;
            li.querySelector("strong").textContent = `${t.amount} ₴`;
            li.querySelector(".t-date").textContent = formattedDate;
            li.querySelector("small").textContent = `${t.comment || 'No comment'}`;

            li.querySelector(".edit-btn").addEventListener("click", () => turnIntoTransactionEditMode(li, t));
            li.querySelector(".delete-btn").addEventListener("click", () => deleteTransaction(t.id));

            list.appendChild(li);
        });
    } catch (error) {
        console.error("Transactions loading error: ", error);
    }
}