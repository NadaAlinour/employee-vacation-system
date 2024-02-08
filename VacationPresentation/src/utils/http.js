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
export async function getVacations(token) {
    const response = await axios.get(`${BASE_URL}/vacation`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });
    return response.data;
}

export async function getVacation(token, id) {
    const response = await axios.get(`${BASE_URL}/vacation/${id}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });
    return response;
}

export async function getRuleHire(token, id) {
    const response = await axios.get(`${BASE_URL}/hire/${id}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });
    return response.data;
}

export async function getRule(token, id) {
    const response = await axios.get(`${BASE_URL}/rule/${id}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });
    return response.data;
}

export async function deleteVacation(token, id) {
    const response = await axios.delete(`${BASE_URL}/vacation/${id}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });
    return response;
}

export async function getUnits(token) {
    const response = await axios.get(`${BASE_URL}/unit`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });
    return response.data;
}

export async function getUnit(token, id) {
    const response = await axios.get(`${BASE_URL}/unit/${id}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });
    return response.data;
}

export async function updateVacation(token, id, vacation) {
    const response = await axios.put(`${BASE_URL}/vacation/${id}`, vacation, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });
    return response.data;
}


/* requests requests ;) */
export async function addRequest(token, request) {
    const response = await axios.post(`${BASE_URL}/request`, request, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    });

    return response.data;
}



/* stats requests */

/* reports requests */

