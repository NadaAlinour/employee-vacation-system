<template>

    <div class="employee-info">

        <h2 class="form-title">View Employees</h2>

        <table class="employee-table">
            <tr>
                <!--<th></th>-->
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

            <tr v-for="employee in employees1" :key="employee.EmployeeID">
                <td>{{employee.employeeID}}</td>
                <td><img class="user-pic" :src="`data:image/png;base64,${employee.image}`" /></td>
                <td>{{employee.name}}</td>
                <td>{{(employee.hiredDate).split('T')[0]}}</td>
                <td>{{employee.phone}}</td>
                <td>{{employee.email}}</td>
                <td>{{employee.salary}}.00 EGP</td>
                <td>{{employee.isVacationAllowed == true ? 'Allowed' : 'Not allowed'}}</td>
                <td><button class="update-btn" @click="handleUpdate(employee.employeeID)">Update</button></td>
                <td @click="handleDelete(employee.employeeID)"><div class="trash-div"></div></td>
            </tr>

        </table>


        <div class="update-emp-container">
            <!-- with employee id as prop -->
            <!-- send id prop -->
            <UpdateEmployeeModal v-if="updateFlag" @close-modal="updateFlag = false" 
                                 :empID="currentEmployeeID" :employee="employeeToBeUpdated"
                                 :image="`data:image/png;base64,${employeeToBeUpdated.image}`"/>

        </div>

    </div>

</template>


<script>

    import PaginationItem from '../components/PaginationItem.vue'
    import SearchbarItem from '../components/SearchbarItem.vue'
    import UpdateEmployeeModal from '../components/UpdateEmployeeModal.vue'

    import { mapActions, mapGetters } from "vuex";


    export default {

        components: {
            PaginationItem,
            SearchbarItem,
            UpdateEmployeeModal,
        },


        data() {
            return {
              
                employeesTemp: [],

                updateFlag: false,

                currentEmployeeID: "",

                employees1: [],

                employeeToBeUpdated : {
                    empID: "",
                    name: "",
                    email: "",
                    image: "",
                    hiredDate: "",
                    phone: "", 
                    vacationPerm: "",
                    salary: ""

                },

                tempSrc: "",
  
            }
        },

        computed: {
            ...mapGetters(["employees"]),

            employees() {
                return this.$store.getters.employees;
            }
        },

        async created() {
            await this.fetchEmployees();
            console.log(this.employees1);
   
        },


        methods: {

            async fetchEmployees() {

                console.log(this.$store.state.token, "test")

                var settings = {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${this.$store.state.token}`
                    }
                }

                try {
                    var byteArray;
                    const response = await fetch('http://localhost:5242/api/employee', settings);
                    console.log(response.status);
                    if (response.status === 401) {
                        console.log("unauthorized action, navigate to unauthorized page");
                        this.$router.push('/401');
                        return;
                    }

                    const data = await response.json();

                    this.employees1 = data;

                    console.log(this.employees1)
                    //console.log(byteArr) this is a valid image
                  /*  var blob = new Blob([new Uint8Array(byteArr)], { type: 'image/png' });
                    this.tempSrc = URL.createObjectURL(blob); */

                    //this.tempSrc = `data: image / png; base64, ${ byteArr }`;
                    //console.log(this.tempSrc)

                } catch (error) {
                    console.log(error.message);
                }
            },


            ...mapActions(["fetchAllEmployees"]),


            async handleUpdate(empId) {
                this.currentEmployeeID = "";
                console.log("update was clicked with employee id of:", empId);
                this.updateFlag = true;
                this.currentEmployeeID = empId;

                var settings = {
                    method: 'GET',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${this.$store.state.token}`
                    }
                }
                // do update things here and send fetched employee data as prop to child component
                try {
                    const response = await fetch('http://localhost:5242/api/employee/' + this.currentEmployeeID, settings);
                    console.log(response.status);
                    if (response.status === 401) {
                        console.log("action unauthorized, navigate to unauthorized page");
                        this.$router.push('/401');
                    }

                    const data = await response.json();

                    console.log(data);

                    this.employeeToBeUpdated.empID = data.employeeID;
                    this.employeeToBeUpdated.name = data.name,
                    this.employeeToBeUpdated.email = data.email,
                    this.employeeToBeUpdated.image = data.image,
                    this.employeeToBeUpdated.hiredDate = data.hiredDate,
                    this.employeeToBeUpdated.phone = data.phone,
                    this.employeeToBeUpdated.vacationPerm = data.isVacationAllowed,
                    this.employeeToBeUpdated.salary = data.salary


                    console.log(this.employeeToBeUpdated.vacationPerm);


                } catch (error) {
                    console.log(error.message)
                }

             
                   
            },

            handleDelete(empId) {
                console.log("deleting employee with id: " + empId);

                fetch('http://localhost:5242/api/employee/' + empId, {
                    method: 'DELETE',
                    headers: {
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${this.$store.state.token}`
                    }
                }).then(res => {
                    res.json();
                    console.log(res.status);
                    if (res.status === 401) {
                        console.log("action unauthorized, navigate to unauthorized page");
                        this.$router.push('/401');
                    }
                }

                )
                    .then(data => console.log(data))
                .catch(err => console.log(err.message))
            },



        }
    }


</script>

<style>

    .employee-info {
        width: 85%;
        height: 100vh;
        position: absolute;
        right: 0;
        background-color: white;
    }

        .employee-info h2 {
            margin-top: 20px;
        }

  
    .employee-table td:nth-child(8){
        text-align: center;
    }

    .employee-table th:nth-child(5) {
        text-align: center;
    }


    .employee-table th:nth-child(6) {
        text-align: center;
    }

    .done-btn {
        margin-left: 1210px;
        background-color: #2ea63b;
    }

        .done-btn:hover {
            background-color: #1e6b27;
        }

    .user-pic {
        width: 50px;
        height: auto;
    }

    .add-emp-btn {
        position: absolute;
        right: 0;
        margin-right: 25px;
    }


    .update-btn {
        margin-right: 10px;
    }

    .delete-btn {
        background-color: #fa8a34;
    }
</style>

