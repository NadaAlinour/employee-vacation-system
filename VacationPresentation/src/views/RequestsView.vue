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

                <div class="add-req-btn">
                    <button type="submit">Add Request</button>
                </div>

                <div class="msg-container">

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

            </div>

        </form>

    </div>

</template>

<script>
    import { getVacations, addRequest } from "../utils/http";

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

        async mounted() {
            // fetch vacation names to populate select options
            try {
                const data = await getVacations(this.$store.state.token);
                this.vacations = data;
            } catch (error) {
                console.log(error);
            }
          
        },

        methods: {


            async submitForm() {
                console.log("request submit is clicked");

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

                const body = {
                    EmployeeID: this.empID,
                    VacationID: this.selectedVacID,
                    StartDate: startDate,
                    ExpectedEndDate: expEndDate
                    //ActualEndDate: "2023-12-01"
                }

                try {
                    const data = await addRequest(this.$store.state.token, body);
                    if (data.errorMessage != null) {
                        console.log(data.errorMessage);
                        this.errMsg = data.errorMessage;
                        this.isValid = false;
                    } else {
                        this.successMsg = data.message;
                        this.isSuccess = true;
                    }

                } catch (error) {
                    console.log(error);
                }
              

            },

        },

    }

</script>

<style>


    .add-req-container {
        height: 100%;
        width: 100%;
        
    }

        .add-req-container h2 {
            margin: auto;
            margin-top: 20px;
        }

    .add-req-form {
        margin-top: 50px;
        display: flex;
        flex-direction: column;
        justify-content: space-between;
    }

    .req-container-1 {
        display: flex;
        flex-direction: column;
        width: 50%;
        align-items: center;
        
    }

    .req-container-2 {
        display: flex;
        flex-direction: column;
        width: 50%;
        align-items: center;
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

        @media(max-width: 670px) {
            .req-container-parent {
                flex-wrap: wrap;
            }
        }

        

    .add-req-container button {
        border-radius: 0px;
        padding-top: 0px;
        padding-bottom: 0px;
        height: 60px;
    }

    .msg-container {
        width: 100%;
    }


    .btn-err-req {
        display: flex;
        align-items: center;
        justify-content: left;
        gap: 15px;
        padding-left: 140px;
    }

    .btn-err-req .err-msg-container {
        width: 30%;
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
        width: 250px;
        margin-top: 10px;
        margin-bottom: 10px;
        padding: 20px;
        border-width: 3px;
    }

        .error-msg2 p {
            color: black;
        }

     

</style>