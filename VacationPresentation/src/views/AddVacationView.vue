<template>
    <div class="add-vac-container">
        <h2 class="form-title">Add Vacation</h2>

        <form class="add-vac-form" @submit.prevent="submitForm">


            <label>Name</label>
            <input type="text" placeholder="Name" v-model="vacation.name" />


            <div class="radio-group-duration">
                <fieldset id="duration-group">
                    <div class="radio-one">
                        <input type="radio" @click="handleDuration" value="true" name="duration-group" />
                        <label>Set duration</label>
                    </div>

                    <div class="radio-one">

                        <input type="radio" @click="handleDuration" value="false" name="duration-group" />
                        <label>Depends on rule</label>
                    </div>

                </fieldset>
            </div>


            <div class="duration-container" v-if="showDuration">
                <label>Duration <small>in days</small></label>
                <input type="number" placeholder="1" min="1" v-model="rule.duration" />
            </div>

            <!----------------------------------------------------------------------------------------------->
            <label>Description</label>
            <input type="text" placeholder="Description" v-model="vacation.description" />

            <!-- conditional rendering -->
            <label>Rules</label>



            <div class="radio-group-1">
                <fieldset id="group1">
                    <div class="radio-one">
                        <input type="radio" name="group1" @click="renderGroupOne" value="true" />
                        <label>Based on hire date</label>
                    </div>

                    <div class="radio-two">

                        <input type="radio" name="group1" @click="renderGroupOne" value="false" />
                        <label>Other</label>
                    </div>

                </fieldset>
            </div>

            <div class="hire-rule-container-parent" v-if="this.toggleOptions1 == true">
                <div class="hire-rule-container" v-for="index in count" :key="index">


                    <label>Days</label>
                    <input type="number" min="1" placeholder="1" @change="handleDays($event)" />

                    <label>From <small>in years</small></label>
                    <input type="number" @change="handleStartYrs($event)" />


                    <label>To <small>in years</small></label>
                    <input type="number" @change="handleEndYrs($event)" />


                </div>

                <button id="add-btn" type="button" @click="addHireRule">+</button>

            </div>


            <div class="option-group-2" v-if="toggleOptions1 == false">
                <label>Number of times</label>
                <input type="number" min="1" placeholder="1" v-model="rule.numberOfTimes" />

                <!------------------------------------------------------------------------------------------------->


                <fieldset id="group2">

                    <div class="radio-one">
                        <input type="radio" name="group2" @click="renderGroupTwo" value="true" />
                        <label>Within period</label>
                    </div>

                    <div class="radio-two">

                        <input type="radio" name="group2" @click="renderGroupTwo" value="false" />
                        <label>Other</label>
                    </div>

                </fieldset>
            </div>


            <div class="option-group-3-4" v-if="toggleOptions34 == true">

                <div class="option-group-3" v-if="toggleOptions2 == true">
                    <label>From</label>
                    <input type="date" v-model="rule.startDate" />

                    <label>To</label>
                    <input type="date" v-model="rule.endDate" />
                </div>



                <div class="option-group-4" v-if="toggleOptions2 == false">

                    <label>Per</label>
                    <select v-model="selectedUnit">
                        <option value="" disabled>Select unit</option>
                        <option v-for="unit in this.units" :key="unit.unitID" :value="unit.unitID">{{unit.unitName}}</option>

                    </select>
                </div>

            </div>


            <div class="btn-msg2">
                <div class="btn2">
                    <button type="submit">Add Vacation</button>
                </div>

                <div class="success-msg-cont">
                    <h3>{{this.successMsg}}</h3>
                </div>

                <div class="err-msg-cont4">
                    <h3>{{this.errMsg}}</h3>
                </div>

            </div>



        </form>

    </div>


</template>

<script>

    export default {

        created() {
            this.fetchUnits();
        },

        data() {
            return {
                count: 1,
                addHireRuleCount: 0,
                showDuration: false,
                showMessage: false,
                successMsg: "",
                errMsg: "",

                selectedUnit: "",

                toggleOptions1: null,
                toggleOptions2: null,
                toggleOptions34: false,

                units: [],


                vacation: {
                    name: null,
                    description: null,
                    isBasedOnHire: false
                },

                hireObject: {
                    numberOfDays: null,
                    startYears: null,
                    endYears: null,
                },

                hire: [],

                rule: {
                    numberOfTimes: null,
                    duration: null,
                    unitID: null,
                    startDate: null,
                    endDate: null
                },


            }
        },


        methods: {

            handleDays(event) {
                this.hireObject.numberOfDays = event.target.value;
            },

            handleStartYrs(event) {
                this.hireObject.startYears = event.target.value;

            },

            handleEndYrs(event) {
                this.hireObject.endYears = event.target.value;

            },

            addHireRule() {
                // add copy of object to hire array then reset hireObject values
                // there should be checks though to make sure all fields are filled before trying to add another rule
                // now it only gets added when i click add tho
                // we'd have an extra empty field list
                console.log(this.hireObject);
                this.hire.push({...this.hireObject});
                console.log(this.hire);

                // reset hireObject
                this.hireObject.numberOfDays = null;
                this.hireObject.startYears = null;
                this.hireObject.endYears = null;
                

                this.count += 1;
                
            },

            addLastHireRule() {
                // just don't increment count

                console.log(this.hireObject);
                this.hire.push({ ...this.hireObject });
                console.log(this.hire);

                // reset hireObject
                this.hireObject.numberOfDays = null;
                this.hireObject.startYears = null;
                this.hireObject.endYears = null;
            },

            handleDuration(e) {
                if (e.target.value == "true") {
                    // show and set duration stuff
                    this.showDuration = true;
                } else {
                    this.showDuration = false;
                    this.rule.duration = null;
                }
            },

            renderGroupOne(e) {
                // console.log("a radio button was clicked with value:", e.target.value)
                if (e.target.value === "false") {
                    this.toggleOptions1 = false; // render second option set
                    this.vacation.isBasedOnHire = false;

                    // reset hire rule
                    this.hire.length = 0;

                    // reset based on hire rule field sets
                    this.count = 1;
                }
                else {
                    this.toggleOptions1 = true; // render first option set
                    this.vacation.isBasedOnHire = true;

                    // unrender dates and unit
                    this.toggleOptions34 = false;

                    // reset rule values
                    this.rule.startDate = null,
                    this.rule.endDate = null,
                    this.rule.unitID = null,
                    this.rule.numberOfTimes = null

                }
            },

            renderGroupTwo(e) {
                if (e.target.value === "false") {
                    this.toggleOptions2 = false; // render second option set
                    this.toggleOptions34 = true;
                    

                    // reset dates
                    this.rule.startDate = null;
                    this.rule.endDate = null;

                }

                else {
                    this.toggleOptions2 = true; // render first option set
                    this.toggleOptions34 = true;

                    // reset unit
                    this.rule.unitID = null;
                }
            },

         

            async addVacation() {

                this.successMsg = "";
                this.errMsg = "";

                if (this.hireObject.numberOfDays !== null) {
                    this.addLastHireRule();
                }

                var sDate = this.rule.startDate;
                var eDate = this.rule.endDate;
               

               // console.log(this.vacation);
               // console.log(this.hire);
               // console.log(this.rule);



                try {
                    const response = await fetch('http://localhost:5242/api/vacation', {
                        method: 'POST',
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${this.$store.state.token}`
                        },

                        body: JSON.stringify({
                            // vacationInfo object

                            vacation: {

                                name: this.vacation.name,
                                description: this.vacation.description,
                                isBasedOnHire: this.vacation.isBasedOnHire
                            },

                            hires: this.hire,

                            rule: {

                                numberOfTimes: this.rule.numberOfTimes,
                                duration: this.rule.duration,
                                unitID: this.rule.unitID,
                                startDate: sDate,
                                endDate: eDate
                            }


                        })
                    });

                    const data = await response.json();
                    console.log(data, "ttyfktflyg");
                    if (data.error != null) {
                        this.errMsg = data.error;
                    }

                    else {
                        this.successMsg = data.message;
                    }
                    

                } catch (error) {
                    console.log(error.message);
                }


            },

            async fetchUnits() {

                const settings = {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${this.$store.state.token}`
                    }
                }

                try {
                    const response = await fetch('http://localhost:5242/api/unit', settings);
                    const data = await response.json();

                    this.units = data;
                    //console.log(data);
                    //console.log(this.units);


                } catch (error) {
                    console.log(error.message);
                }
            },



            async submitForm() {
                console.log("add vacation is clicked");

                this.successMsg = "";
                this.showMessage = true;

                await this.addVacation();
            },

         
        }
    }

</script>

<style>

   
    .duration-container {
        display: flex;
        flex-direction: column;
    }


    fieldset {
        border-style: none;
        margin-bottom: 30px;
    }

    .add-vac-container {
        width: 85%;
        height: 100vh;
        position: absolute;
        right: 0;
    }

        .add-vac-container h2 {
            margin: auto;
            margin-top: 20px;
        }

    .add-vac-form {
        margin-top: 50px;
        display: flex;
        flex-direction: column;
        margin-left: 100px;
        margin-right: 50px;
        
    }

        .add-vac-form label {
            margin-bottom: 10px;
            background-color: white;
            font-weight: bold;
            margin-bottom: 5px;
        }

        .add-vac-form input {
            width: 25%;
            padding: 15px;
            border: 1px solid rgba(0,0,0,.2);
            margin-bottom: 30px;
        }

    .add-vac-container button {
        border-radius: 0px;
        padding-top: 0px;
        padding-bottom: 0px;
        height: 60px;
    }

    #group1 {
        border-style: none;
        background-color: white;
        margin-top: 15px;
    }

    .radio-one {
        width: 18%;
    }

    .radio-two {
        width: 18%;
        font-style: italic;
    }

    #group2 {
        border-style: none;
    }

    .option-group-1 {
        display: flex;
        flex-direction: row;
    }


    .option-group-2 {
        display: flex;
        flex-direction: column;
    }

    .option-group-3 {
        display: flex;
        flex-direction: column;
    }

    .option-group-4 {
        background-color: white;
        display: flex;
        flex-direction: column;
    }


        .option-group-4 select {
            width: 28%;
            padding: 15px;
            border-radius: 10px;
            margin-bottom: 50px;
        }

        .option-group-4 option {
            font-size: 1rem;
        }

 

    #add-btn {
        border-radius: 50px;
        font-size: 2rem;
        text-align: center;
        margin-left: 1000px;
    }

    .hire-rule-container {
        display: flex;
        flex-direction: row;
        border-bottom: solid;
        margin-bottom: 20px;
        border-color: rgba(133, 135, 145, 0.3);
    }

        .hire-rule-container label {
            margin-right: 10px;
        }

        .hire-rule-container input {
            margin-right: 20px;
        }

        .btn-msg2 {
            display: flex;
            flex-direction: row;
            gap: 15px;
            align-items: center;
            margin-bottom: 50px;
        }

        .add-vac-container .success-msg-cont {
        }

    .err-msg-cont4 h3 {
        color: #f54245;
    }

       

</style>