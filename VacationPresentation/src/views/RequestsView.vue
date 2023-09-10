<template>

    <div class="add-req-container">
        <h2 class="form-title">Add Request</h2>

        <form class="add-req-form" @submit.prevent="submitForm">

            <div class="req-container-parent">
                <div class="req-container-1">

                    <label>Employee ID</label>
                    <input type="text" placeholder="Employee ID here.." v-model="empID"
                           :class="this.empErr ? 'errField' : ''" />

                    <label>Vacation Type</label>
                    <select v-model="selectedVacID" :class="this.vacErr ? 'errField' : ''">
                        <option value="" disabled>Select vacation</option>
                        <option v-for="vac in vacations" :key="vac.vacationID" :value="vac.vacationID">
                            {{vac.name}}
                        </option>
                    </select>

                    <label>Start Date</label>
                    <input type="date" v-model="startDate"
                           :class="this.startDateErr ? 'errField' : ''" />
                </div>


                <div class="req-container-2">

                    <label>Expected End Date</label>
                    <input type="date" v-model="expEndDate"
                           :class="this.expEndDateErr ? 'errField' : ''" />

                    <label>Actual End Date</label>
                    <input type="date" />

                </div>
            </div>

            <div class="btn-err-req">
                <div>
                    <button type="submit">Add Request</button>
                </div>



                <div class="success-msg-container2" v-if="isSuccess">
                    <div class="success-msg">
                        <p>{{successMsg}}</p>
                    </div>
                </div>

                <div class="err-msg-container">

                    <!-- request refused error -->
                    <div class="error-msg2" v-if="!isValid">
                        <p>{{errMsg}}</p>  <!--do i need to bind something-->
                    </div>

                </div>

            </div>

           

            



        </form>

    </div>

</template>

<script>

    export default {

        data() {
            return {
                vacations: [],
                // make form data into an object later
                empID: "",

                selectedVacType: "",
                selectedVacID: "",

                startDate: "",
                expEndDate: "",



                isValid: true,
                isSuccess: false,
                errMsg: "",
                successMsg: "",

                empErr: false,
                vacErr: false,
                startDateErr: false,
                expEndDateErr: false,

            }
        },

        mounted() {
            // fetch vacation names to populate select options
            const settings = {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${this.$store.state.token}`
                }
            }

            fetch('http://localhost:5242/api/vacation', settings)
                .then(res => res.json())
                .then(data => {
                    this.vacations = data
                    console.log(this.vacations)
                }).catch(err => console.log(err.message))
        },

        methods: {


            submitForm() {
                console.log("request submit is clicked");

               /* console.log(this.isValid, "isValid")
                console.log(this.isSuccess, "isSuccess") */

                this.errMsg = "";
                this.successMsg = "";
                this.isValid = true;
                this.isSuccess = false;

                this.empErr = false;
                this.vacErr = false;
                this.startDateErr = false;
                this.expEndDateErr = false;


                var startDate = this.startDate;
                var expEndDate = this.expEndDate;


                // field validations
                if (this.empID == "")
                    this.empErr = true;

                if (this.selectedVacID == "")
                    this.vacErr = true;

                if (this.startDate == "")
                    this.startDateErr = true;

                if (this.expEndDate == "")
                    this.expEndDateErr = true;

                if (this.empErr || this.vacErr || this.startDateErr || this.expEndDateErr) {

                    this.errMsg = "Please fill in the required fields.";
                    this.isValid = false;
                    return;
                }

                // get vacation id from vacation type


                fetch('http://localhost:5242/api/request', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${this.$store.state.token}`
                    },
                    body: JSON.stringify({
                        EmployeeID: this.empID,
                        VacationID: this.selectedVacID,
                        StartDate: startDate,
                        ExpectedEndDate: expEndDate
                        //ActualEndDate: "2023-12-01"
                    })
                })
                    .then(res => res.json())

                    .then(data => {

                        if (data.errorMessage != null) {
                            console.log(data.errorMessage);
                            this.errMsg = data.errorMessage;
                            this.isValid = false;
                        } else {
                            console.log(data.message, "testestwaesfasdfsdf");
                            this.successMsg = data.message;
                            this.isSuccess = true;
                        }

                        console.log(data)

                    })
                    .catch(err => console.log(err))

               

            },

        },

    }

</script>

<style>


    .add-req-container {
        width: 85%;
        height: 100vh;
        position: absolute;
        right: 0;
        background-color: white;
    }

        .add-req-container h2 {
            margin: auto;
            margin-top: 20px;
        }

    .add-req-form {
        margin-top: 50px;
        display: flex;
        flex-direction: column;
        background-color: white;
        margin-left: 155px;
    }

    .req-container-1 {
        display: flex;
        flex-direction: column;
        width: 50%;
        margin-right: 50px;
        background-color: white;
    }

    .req-container-2 {
        display: flex;
        flex-direction: column;
        width: 50%;
        background-color: white;
    }


    .add-req-form label {
        margin-bottom: 10px;
        background-color: white;
        font-weight: bold;
        margin-bottom: 5px;
    }

    .add-req-form input {
        width: 50%;
        padding: 15px;
        border: 1px solid rgba(0,0,0,.2);
        margin-bottom: 20px;
        
    }

    .add-req-form select {
        width: 56%;
        padding: 15px;
        border: 1px solid rgba(0,0,0,.2);
        margin-bottom: 20px;
    }

        .add-req-form select option {
            background-color: white;
        }

        .req-container-parent {
            display: flex;
        }

    .add-req-container button {
        border-radius: 0px;
        padding-top: 0px;
        padding-bottom: 0px;
        height: 60px;
        /*position: absolute;*/
        /* margin-left: 320px;*/
    }


    .btn-err-req {
        display: flex;
        gap: 15px;
        align-items: center;
    }

    .btn-err-req .err-msg-container {
        width: 75%;
    }

    .btn-err-req .success-msg-container2 {
        width: 75%;
    }

    .success-msg {
        border-style: solid;
        border-color: #63ab63;
        background-color: #9dd49d;
        text-align: center;
        margin-top: 10px;
        margin-bottom: 10px;
        padding: 20px;
        border-width: 3px;
    }

        .success-msg p {
            color: white;
        }

    .error-msg2 {
        border-style: solid;
        border-color: #ff8282;
        background-color: rgba(255, 0, 0, 0.2);
        text-align: center;
        width: 50%;
        margin-top: 10px;
        margin-bottom: 10px;
        padding: 20px;
        border-width: 3px;
    }

        .error-msg2 p {
            color: black;
        }

     

</style>