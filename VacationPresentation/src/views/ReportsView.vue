<template>
    <div class="reports-container">
        <h2 class="form-title">Search by</h2>

        <div class="radio-group-search">
            <fieldset id="search-radio">
                <div class="">
                    <input type="radio" name="search-group" value="employee" @click="handleSearchRadio($event)" />
                    <label>Employee</label>
                </div>

                <div class="">

                    <input type="radio" name="search-group" value="vacation" @click="handleSearchRadio($event)" />
                    <label>Vacation</label>
                </div>

            </fieldset>
        </div>

        <!-- search by vacation -->
        <div v-if="this.searchFlag == false" class="select-vacation-container">
            <select v-model="selectedVacation" :class="vacErr ? 'errField' : ''">
                <option value="" disabled>Select Vacation</option>
                <option v-for="vac in this.vacations" :key="vac.vacationID" :value="vac.vacationID">{{vac.name}}</option>

            </select>
            <button type="button" @click="handleSearchByVacation(this.selectedVacation)">Search</button>
        </div>


        <div v-if="this.showEmployees == true">
            <table class="employee-table">
                <tr>
                    <th>ID</th>
                    <th>Image</th>
                    <th>Name</th>
                    <th>Date Hired</th>
                    <th>Phone</th>
                    <th>Email</th>
                    <th>Salary</th>
                    <th>Vacation Perm</th>
                    <th></th>
                    <th></th>

                </tr>

                <tr v-for="employee in specificEmployees" :key="employee.EmployeeID">
                    <td>{{employee.employeeID}}</td>
                    <td><img class="user-pic2" :src="`data:image/png;base64,${employee.image}`" /></td>
                    <td>{{employee.name}}</td>
                    <td>{{(employee.hiredDate).split('T')[0]}}</td>
                    <td>{{employee.phone}}</td>
                    <td>{{employee.email}}</td>
                    <td>{{employee.salary}}.00 EGP</td>
                    <td>{{employee.isVacationAllowed == true ? 'Allowed' : 'Not allowed'}}</td>
                </tr>

            </table>
        </div>



        <!-- search by employee -->
        <div v-if="this.searchFlag == true" class="employee-input">
            <label>Employee ID:</label>
            <input type="text" placeholder="ID" 
                   v-model="this.empId" id="empIdInput" 
                   :class="this.empErr ? 'errField' : ''"
                   />

            <label>From:</label>
            <input type="date"
                   v-model="this.fromDate"
                   :class="this.fromErr ? 'errField' : ''"
                   />

            <label>To:</label>
            <input type="date" 
                   v-model="this.toDate"
                   :class="this.toErr ? 'errField' : ''"
                   />

            <button type="button" @click="handleSearchByEmployee">Search</button>
        </div>

        <div class="err-msg-cont">
            <h3>{{this.errorMsg}}</h3>
        </div>


        <div v-if="this.showVacations == true">
            <table class="vacation-table">
                <tr>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Rules</th>
                    <th></th>
                </tr>

                <tr v-for="vac in specificVacations" :key="vac.vacationID">
                    <td>{{vac.name}}</td>
                    <td>{{vac.description}}</td>
                    <td>
                        <div class="view-btn-container">
                            <button class="view-btn"
                                    @click="handleViewRules(vac.vacationID)">
                                View
                            </button>
                        </div>
                    </td>
                </tr>

            </table>
        </div>

        <ViewRulesModal v-if="this.showRulesModal == true" :rule="rule" :hire="hire" :flag="isBasedOnHire" @close-modal="this.showRulesModal = false"></ViewRulesModal>

    </div>
</template>

<script>
    import ViewRulesModal from '../components/ViewRulesModal.vue';
import { getRule, getRuleHire, getUnit, getVacations, searchEmployees, searchVacations } from '../utils/http';

    export default {


        components: {
            ViewRulesModal,
        },

        data() {
            return {
                searchFlag: null,
                showEmployees: false,
                showVacations: false,
                showRulesModal: false,

                vacations: [],
                errorMsg: "",

                selectedVacation: "",

                specificVacations: [],
                specificEmployees: [],

                fromDate: null,
                toDate: null,
                empId: null,

                fromErr: false,
                toErr: false,
                empErr: false,
                vacErr: false,

                // view rules variables
                isBasedOnHire: false,
                rule: {
                    numberOfTimes: null,
                    duration: null,
                    unitID: null,
                    unitName: null,
                    startDate: null,
                    endDate: null
                },
                hire: [],
            }
        },

        created() {
            this.fetchAllVacations();
        },

        methods: {
            handleSearchRadio(event) {
                console.log("click", event.target.value);
                this.errorMsg = "";
                this.empErr = false;
                this.toErr = false;
                this.fromErr = false;
                this.vacErr = false;
                

                if (event.target.value == "employee")
                    this.searchFlag = true;

                else if (event.target.value == "vacation")
                    this.searchFlag = false;

                this.showEmployees = false;
                this.showVacations = false;

            },

            handleSearchByVacation(vacId) {
                console.log("searching by vacation");
                this.errorMsg = "";
                this.vacErr = false;

                if (this.selectedVacation == "") {
                    this.errorMsg = "Please select a vacation.";
                    this.vacErr = true;
                    return;
                }
                this.showEmployees = true;
                this.fetchEmployees(vacId);
            },

            async handleSearchByEmployee() {
                console.log("searching by employee");

                this.errorMsg = "";
                this.showVacations = false;
                this.empErr = false;
                this.fromErr = false;
                this.toErr = false;

                // field validation
                if (this.empId === null || this.empId === "")
                    this.empErr = true;
                if (this.fromDate === null || this.fromDate === "")
                    this.fromErr = true;
                if (this.toDate === null || this.toDate === "")
                    this.toErr = true;

                    console.log(this.empId, this.fromDate, this.toDate)

                if (this.empErr || this.fromErr || this.toErr == true) {
                    console.log("error cant proceed")
                    this.errorMsg = "Please fill in all the fields.";
                    return;
                } else if (this.fromDate > this.toDate) {
                    this.fromErr = true;
                    this.toErr = true;
                    this.errorMsg = "Start date must be smaller than end date.";
                    return;
                }

             

                await this.fetchVacations();
                console.log(this.errorMsg)

                if (this.errorMsg != "")
                    this.showVacations = false;

                else this.showVacations = true;


            },

            handleViewRules(vacID) {
                console.log("view was clicked with vacation id = ", vacID);
                this.showRulesModal = true;

                console.log(this.vacations);
                for (var i = 0; i < this.vacations.length; i++) {
                    if (this.vacations[i].vacationID === vacID) {
                        this.isBasedOnHire = this.vacations[i].isBasedOnHire;
                    }
                }

                console.log(this.rule.unitID)

                if (this.isBasedOnHire == true) {
                    //fetch hire rules
                    this.fetchVacationHire(vacID);
                    
                } else if (this.isBasedOnHire == false) {
                    //fetch other rules
                    this.fetchVacationRule(vacID);

                }

            },

            async fetchUnit() {

                try {
                    const data = await getUnit(this.$store.state.token, this.rule.unitID);
                    this.rule.unitName = data.unitName;
                    console.log(this.rule.unitName);
                } catch (error) {
                    console.log(error.message);
                }
            },

            async fetchAllVacations() {

                try {
                    const data = await getVacations(this.$store.state.token);
                    console.log(data);
                    this.vacations = data;
                } catch (error) {
                    console.log(error.message);
                }
            },

            async fetchEmployees(vacId) {

                try {
                    const response = await searchEmployees(this.$store.state.token, vacId);

                    if (response.status === 401) {
                        console.log("action unauthorized, navigate to unauthorized page");
                        this.$router.push('/401');
                        return;
                    }

                    const data = response.data;
                    console.log(data);
                    this.specificEmployees = data;

                } catch (error) {
                    console.log(error.message);
                }
            },

            async fetchVacations() {

                // might need to do the date thing
                var startDate = this.fromDate;
                var endDate = this.toDate;


                const body = {
                    StartDate: startDate,
                    EndDate: endDate
                }

                try {
                    const response = await searchVacations(this.$store.state.token, this.empId, body);

                    if (response.status === 401) {
                        console.log("action unauthorized, navigate to unauthorized page");
                        this.$router.push('/401');
                        return;
                    }

                    const data = response.data;
                    console.log(data);

                    if (data.message) {
                        this.errorMsg = data.message;

                    } else this.specificVacations = data;

                } catch (error) {
                    console.log(error);
                }


            },

            async fetchVacationRule(vacID) {

                try {
                    const data = await getRule(this.$store.state.token, vacID);
                    console.log(data);
                 
                    this.rule.numberOfTimes = data.numberOfTimes;
                    this.rule.duration = data.duration;
                    this.rule.unitID = data.unitID;
                    this.rule.startDate = data.startDate;
                    this.rule.endDate = data.endDate;

                    console.log(this.rule);

                } catch (error) {
                    console.log(error.message);
                }

                if (this.rule.unitID !== null) {
                    this.fetchUnit();
                }
            },

            async fetchVacationHire(vacID) {

                try {
                    const data = await getRuleHire(this.$store.state.token, vacID);
                    console.log(data);
                    this.hire = data;
                    console.log(this.hire);
                } catch (error) {
                    console.log(error.message);
                }
            }
        },


    }
</script>

<style>
    .reports-container {
        width: 85%;
        height: 100vh;
        position: absolute;
        right: 0;
        background-color: white;
        text-align: center;
    }

        .reports-container h2 {
            margin-top: 20px;
            font-size: 2rem;
            margin-bottom: 50px;
        }

        .reports-container .vacation-table {
            margin-top: 30px;
        }

        .reports-container .vacation-table {
            width: 75%;
            text-align: center;
        }

       
        .reports-container .employee-table {
            width: 85%;
            text-align: center;
            
        }

        .user-pic2 {
            width: 50px;
        }

    #search-radio {
        border-style: none;
        display: flex;
        justify-content: center;
        gap: 50px;
        margin-bottom: 45px;
    }

    #search-radio label {
        margin-left: 10px;
        font-size: 1.5rem;
    }
    
    .employee-input {
        display: flex;
        justify-content: center;
        align-items: center;
        gap: 20px;
        
    }

    .employee-input label {
        margin-right: -10px;
    }

        .employee-input input {
            padding: 15px;
            border: 1px solid rgba(0,0,0,.2);
            text-align: center;
        }

        #empIdInput {
            width: 5%;
        }

    .select-vacation-container {
        display: flex;
        justify-content: center;
        align-items: center;
        gap: 30px;
    }

        .select-vacation-container select {
            width: 21%;
            padding: 15px;
            border: 1px solid rgba(0,0,0,.2);
        }

            .select-vacation-container option {
                font-size: 1rem;
            }

            .reports-container .view-btn-container {
                text-align: center;
            }

            .err-msg-cont {
                margin-top: 40px;
            }


                .err-msg-cont h3 {
                    color: #f54245;
                }
   

</style>