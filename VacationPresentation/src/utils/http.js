import axios from 'axios';

const BASE_URL = 'http://localhost:5242/api';

/* authentication requests */
export async function login(email, password) {
    const user = {
        email: email,
        password: password
    }
    const response = await axios.post(`${BASE_URL}/login`, user, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },

    })
    return response.data;
}


/* employee requests */
export async function getEmployees(token) {
    const response = await axios.get(`${BASE_URL}/employee`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    })
    return response.data;
}

export async function getEmployee(token, id) {
    const response = await axios.get(`${BASE_URL}/employee/${id}`,
        {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            }

        });
    return response.data;
}

export async function addEmployee(token, employee) {
    const response = await axios.post(`${BASE_URL}/employee/`, employee, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'multipart/form-data',
            accept: 'application/json'
        },
    });
    return response.data;
}

export async function updateEmployee(token, id, employee) {
    const response = await axios.put(`${BASE_URL}/employee/${id}`, employee, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Authorization': `Bearer ${token}`,
            'Content-Type': 'multipart/form-data',
            accept: 'application/json'
        },
    });
    return response.data;
}



export async function deleteEmployee(token, id) {
    const response = await axios.delete(`${BASE_URL}/employee/${id}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    })
    return response.data;
}


/* vacation requests */
/* requests requests ;) */
/* stats requests */
/* reports requests */

