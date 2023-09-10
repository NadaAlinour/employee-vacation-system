<template>
    <div class="modal-overlay" @click="$emit('close-modal')">
        <div class="update-vac-modal" @click.stop>
            <form @submit.prevent="submitForm">
                <h2 class="form-title">Update Vacation</h2>

                <div class="container">
                    <div class="update-vac-container">
                        <label>Name</label>
                        <input type="text" placeholder="Name" v-model="vacation.vacation.name"/>


                        <!-- if duration not equals "" -->
                        <div v-if="vacation.isBasedOnHire == false">
                            <label>Duration <small>in days</small></label>
                            <input type="number" min="1" v-model="vacation.rule.duration" />
                        </div>

                        <label>Description</label>
                        <input type="text" placeholder="Description" v-model="vacation.vacation.description"/>


                        <div class="upd-vac-btn">
                            <button type="submit">Update</button>

                            <div class="success-msg-cont">
                                <h3>{{this.successMsg}}</h3>
                            </div>

                            <div class="err-msg-cont5">
                                <h3>{{this.errMsg}}</h3>
                            </div>

                        </div>



                    </div>


                    <div class="update-rules-container">

                        <div class="update-hire-rules-container" >

                            <div class="hire-rules-container" v-if="toggle.toggleHire == true" v-for="h in this.vacation.hires" :key="h.hireID">

                                <label>Days</label>
                                <input type="number" min="1" 
                                       v-model="h.numberOfDays"/>

                                <label>From <small>in years</small></label>
                                <input type="number"
                                       v-model="h.startYears"/>

                                <label>To <small>in years</small></label>
                                <input type="number"
                                       v-model="h.endYears"/>

                            </div>

                            <div class="rules-period-container" v-if="toggle.togglePeriod == true">
                                <div class="numtimes-div">
                                    <label>Number of times</label>
                                    <input type="number" v-model="vacation.rule.numberOfTimes" />
                                </div>
                                
                                <div class="period-div">
                                    <label>From</label>
                                    <input type="date" v-model="vacation.rule.startDate" />

                                    <label>To</label>
                                    <input type="date" v-model="vacation.rule.endDate" />
                                </div>
                            </div>

                            <div class="rules-unit-container" v-if="toggle.toggleUnit == true">
                                <div class="numtimes-div">
                                    <label>Number of times</label>
                                    <input type="number" v-model="vacation.rule.numberOfTimes" />
                                </div>
                                <div class="unit-container">
                                    <label>Per</label>
                                    <select v-model="this.selectedUnit">
                                        <option v-for="unit in this.units" :key="unit.unitID" :value="unit.unitID">{{unit.unitName}}</option>

                                    </select>
                                </div>
                            </div>

                        </div>

                    </div>

        
                </div>

                <button class="exit-btn" type="button" @click="$emit('close-modal')" id="vac-exit-btn">Exit</button>
            </form>
        </div>
    </div>
</template>

<script>
    export default {

        props: ['vacation', 'toggle', 'units', 'vacId'],

        data() {
            return {
                selectedUnit: this.vacation.rule.unitID,

                newHire: [],

                hireObject: {
                    numberOfDays: null,
                    startYears: null,
                    endYears: null,
                },

                successMsg: "",
                errMsg: ""
               
            }
        },

        created() {
            console.log(this.vacation)
        },

        methods: {

            submitForm() {
                console.log("update vacation is clicked");

                this.successMsg = "";
                this.errMsg = "";

                //console.log(this.vacation.hires);
                var sDate = this.vacation.rule.startDate;
                var eDate = this.vacation.rule.endDate;
                //console.log(this.vacation);

                for (var i = 0; i < this.vacation.hires.length; i++) {
                    this.hireObject.numberOfDays = this.vacation.hires[i].numberOfDays;
                    this.hireObject.startYears = this.vacation.hires[i].startYears;
                    this.hireObject.endYears = this.vacation.hires[i].endYears;

                    this.newHire.push({ ...this.hireObject });
                }

                //console.log(this.newHire);
                //console.log(this.vacation.isBasedOnHire, this.vacation.vacation.description)
                console.log(this.vacation.rule.startDate);
                console.log(this.vacation.rule.endDate);
                console.log(this.vacation.vacation.name)


                fetch('http://localhost:5242/api/vacation/' + this.vacId, {
                    method: 'PUT',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${this.$store.state.token}`
                    },
                    body: JSON.stringify({

                        vacation: {

                            name: this.vacation.vacation.name,
                            description: this.vacation.vacation.description,
                            isBasedOnHire: this.vacation.isBasedOnHire
                        },

                        hires: this.newHire,

                        rule: {

                            numberOfTimes: this.vacation.rule.numberOfTimes,
                            duration: this.vacation.rule.duration,
                            unitID: this.selectedUnit,
                            startDate: sDate,
                            endDate: eDate
                        }
                    })
                })
                    .then(res => res.json())
                    .then(data => {
                        console.log(data);

                        if (data.error != null) {
                            this.errMsg = data.error;
                        }

                        else this.successMsg = data.message;

                    })
                    .catch(err => console.log(err.message));

            },
        }
       
    }
</script>

<style>

    .update-vac-modal {
        text-align: center;
        height: 635px;
        width: 800px;
        margin-top: 1%;
        margin-left: 15%;
        padding: 60px 0;
        border-radius: 20px;
    }

    .container {
        display: flex;
        flex-direction: row;
        text-align: left;
    }

    .update-vac-container {
        display: flex;
        flex-direction: column;
        width: 50%;
        margin: 20px;
        margin-left: 40px;
    }

    .container label {
        margin-bottom: 10px;
    }

    .container input {
        width: 270px;
        padding: 15px;
        border: 1px solid rgba(0,0,0,.2);
        margin-bottom: 15px;
    }

    .update-rules-container {
        width: 50%;
        display: flex;
        flex-direction: column;
    }

    .update-hire-rules-container {
        display: flex;
        flex-direction: column;
        margin: 20px;
    }

 
    #vac-exit-btn {
        color: black;
        background-color: white;
        border-style: solid;
        border-width: 2px;
        padding-top: 13px;
        padding-bottom: 13px;
        margin-top: 175px;

    }

    #vac-exit-btn:hover {
        background-color: #ebedee;
        color: black;
    }

    .numtimes-div {
        display: flex;
        flex-direction: column;
    }

    .period-div {
        display: flex;
        flex-direction: column;
    }

  .unit-container {
     display: flex;
     flex-direction: column;
  }


    .unit-container select {
        width: 300px;
        padding: 15px;
        border: 1px solid rgba(0,0,0,.2);
    }

    .unit-container option {
        font-size: 1rem;
    }

    .hire-rules-container {
        display: flex;
        gap: 10px;
        margin-right: 20px;
        margin-top: 25px;
        align-items: center;
    }

    .hire-rules-container input {
        width: 35px;
    }

    .upd-vac-btn {
        display: flex;
        gap: 15px;
    }

    .err-msg-cont5 h3 {
        color: #f54245;
    }

</style>