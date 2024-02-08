<template>

    <div class="vacation-info">

        <h2 class="form-title">View Vacations</h2>


        <table class="vacation-table">
            <tr>
                <th>Name</th>
                <th>Description</th>
                <th>Rules</th>
                <th></th>
                <th></th>


            </tr>

            <tr v-for="vacation in vacations" :key="vacation.vacationID">
                <td>{{vacation.name}}</td>
                <td>{{vacation.description}}</td>
                <td>
                    <div class="view-btn-container">
                        <button class="view-btn"
                                @click="handleView(vacation.vacationID)"
                                @close-modal="modalFlag1 = false">
                            View
                        </button>
                    </div>
                </td>
                <td>
                    <button class="update-btn"
                            @click="handleUpdate(vacation.vacationID)"
                            @close-modal="updateShow = false">
                        Update
                    </button>
                </td>

                <td @click="handleDelete(vacation.vacationID)">
                    <div class="trash-div"></div>
                </td>


            </tr>

        </table>

        <div>

            <ViewRulesModal v-if="modalFlag1" @close-modal="modalFlag1 = false" :rule="rule" :hire="hire" :flag="isBasedOnHire" />

        </div>


        <div>
            <UpdateVacationItem v-if="updateShow"
                                @close-modal="updateShow = false"
                                :vacation="this.vacationToBeUpdated"
                                :toggle="this.toggleVacationRule"
                                :units="this.units"
                                :vacId="this.vacId">

            </UpdateVacationItem>

        </div>



    </div>



</template>


<script>

    import PaginationItem from '../components/PaginationItem.vue'
    import SearchbarItem from '../components/SearchbarItem.vue'
    import ViewRulesModal from '../components/ViewRulesModal.vue'
    import UpdateVacationItem from '../components/UpdateVacationItem.vue'
    import { getVacations, getVacation, getRule, getRuleHire, getUnits, getUnit, deleteVacation } from "../utils/http";

    export default {

        components: {
            PaginationItem,
            SearchbarItem,
            ViewRulesModal,
            UpdateVacationItem,
        },

        data() {
            return {
                vacations: [],
                units: [],
                vacId: null,

                vacationToBeUpdated: {
                    vacation: {
                        name: null,
                        description: null,
                        isBasedOnHire: null
                    },

                    hires: [],

                    rule: {
                        numberOfTimes: null,
                        duration: null,
                        unitID: null,
                        startDate: null,
                        endDate: null
                    }
                },

                toggleVacationRule: {
                    toggleHire: false,
                    togglePeriod: false,
                    toggleUnit: false

                },

                modalFlag1: false,
                modalFlag2: false,
                updateShow: false,

                isBasedOnHire: false,

                rule: {
                    numberOfTimes: "",
                    duration: "",
                    unitID: "",
                    unitName: "",
                    startDate: "",
                    endDate: ""
                },

                hire: [],

            }
        },

        async mounted() {

            try {
                const data = await getVacations(this.$store.state.token);
                this.vacations = data;
            } catch (error) {
                console.log(error);
            }
           
        },


        methods: {

            async fetchData(vacID) {

                const settings = {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${this.$store.state.token}`
                    }
                }

                try {
                    const data = await getRule(this.$store.state.token, vacID);

                    console.log(data);

                    this.rule.numberOfTimes = data.numberOfTimes;
                    this.rule.duration = data.duration;
                    this.rule.unitID = data.unitID;

                    if (data.startDate != null) {
                        this.rule.startDate = data.startDate.split('T')[0];
                    } else this.rule.startDate = null;

                    if (data.endDate != null) {
                        this.rule.endDate = data.endDate.split('T')[0];
                    } else this.rule.endDate = null;


                   /* this.rule.startDate = data.startDate.split('T')[0];
                    this.rule.endDate = data.endDate.split('T')[0];*/

                    console.log(this.rule, "test");

                } catch (error) {
                    console.log(error.message);
                }

                // fetch unit by unitID
                if (this.rule.unitID != null) {
                    try {

                        const data = await getUnit(this.$store.state.token, this.rule.unitID);

                        console.log(data);
                        this.rule.unitName = data.unitName;

                    } catch (error) {
                        console.log(error.message);
                    }
                }
            },


            async handleView(vacId) {
                this.modalFlag1 = true;

                // get rule for specific vacation
                // console.log(vacId);

                // get specific vacation isBasedOnHire property
                for (var i = 0; i < this.vacations.length; i++) {
                    if (this.vacations[i].vacationID === vacId)
                        this.isBasedOnHire = this.vacations[i].isBasedOnHire;
                }

               

                if (this.isBasedOnHire) {
                    // send req to get hire rule
                    try {
                        const data = await getRuleHire(this.$store.state.token, vacId);
                        this.hire = data;
                    } catch (error) {
                        console.log(error);async
                    }
                   

                } else {
                    this.fetchData(vacId);
                }

            },

            async handleUpdate(vacId) {
                var isBasedOnHire = null;
                this.toggleVacationRule.toggleHire = false;
                this.toggleVacationRule.togglePeriod = false;
                this.toggleVacationRule.toggleUnit = false;

                this.vacId = vacId;

                try {
                    const response = await getVacation(this.$store.state.token, vacId);

                    if (response.status === 401) {
                       console.log("action unauthorized, navigate to unauthorized page");
                       this.$router.push('/401');
                       return;
                   }

                    this.vacationToBeUpdated.vacation.name = response.data.name;
                    this.vacationToBeUpdated.vacation.description = response.data.description;
                    this.vacationToBeUpdated.isBasedOnHire = response.data.isBasedOnHire;

                    isBasedOnHire = response.data.isBasedOnHire;

                } catch (error) {
                    console.log(error.message);
                   
                }


                if (isBasedOnHire == true) {
                    // get hire rule
                    try {
                        const data = await getRuleHire(this.$store.state.token, vacId);
                        this.vacationToBeUpdated.hires = data;


                        this.toggleVacationRule.toggleHire = true;
                    } catch (error) {
                        console.log(error);
                    }
                   

                } else if (isBasedOnHire == false) {
                    try {
                        const data = await getRule(this.$store.state.token, vacId);
                        // this.hire = data;
                        this.vacationToBeUpdated.rule.numberOfTimes = data.numberOfTimes;
                        this.vacationToBeUpdated.rule.duration = data.duration;
                        this.vacationToBeUpdated.rule.unitID = data.unitID;
                        if (data.startDate != null) {
                            this.vacationToBeUpdated.rule.startDate = data.startDate.split('T')[0];
                        } else this.vacationToBeUpdated.rule.startDate = null;

                        if (data.endDate != null) {
                            this.vacationToBeUpdated.rule.endDate = data.endDate.split('T')[0];
                        } else this.vacationToBeUpdated.rule.endDate = null;

                        if (data.unitID == null)
                            this.toggleVacationRule.togglePeriod = true;

                        else this.toggleVacationRule.toggleUnit = true;
                    } catch (error) {
                        console.log(error.message);
                    }
                     
                        

                    if (this.toggleVacationRule.toggleUnit == true) {
                        //fetch units
                        try {
                            const data = await getUnits(this.$store.state.token);
                            this.units = data;
                            console.log(data);

                        } catch (error) {
                            console.log(error.message);
                        }
                    }

                }


                this.updateShow = true;
            
                },

            async handleDelete(vacId) {
                console.log("deleting vacations with id: " + vacId);
                try {
                    const response = await deleteVacation(this.$store.state.token, vacId);
                    if (response.status === 401) {
                        console.log("action unauthorized, navigate to unauthorized page");
                        this.$router.push('/401');
                    }
                } catch (error) {
                    console.log(error);
                }

            },

        },


        }


</script>

<style>

    .vacation-info {
        width: 85%;
        height: 100vh;
        position: absolute;
        right: 0;
        background-color: white;
    }

        .vacation-info h2 {
            margin-top: 20px;
        }

    .vacation-table th:nth-child(3) {
        text-align: center;
    }

</style>

