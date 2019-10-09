import React, { Component } from 'react';

export class FetchData extends Component {
  displayName = FetchData.name

  constructor(props) {
    super(props);
    this.state = { employees: [], loading: true };
    fetch('api/SampleData/GetEmployees')
      .then(response => response.json())
        .then(data => {
          this.setState({ employees: data, loading: false });
      });
  }

    static renderEmployeesTable(employees) {
    return (
      <table className='table'>
        <thead>
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th>ContractTypeName</th>
            <th>RoleName</th>
            <th>HourlySalary</th>
            <th>MonthlySalary</th>
            <th>AnnualSalary</th>
          </tr>
        </thead>
        <tbody>
          {employees.map(employee =>
            <tr key={employee.id}>
              <td>{employee.id}</td>
              <td>{employee.name}</td>
              <td>{employee.contractTypeName}</td>
              <td>{employee.roleName}</td>
              <td>{employee.hourlySalary}</td>
              <td>{employee.monthlySalary}</td>
              <td>{employee.annualSalary}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : FetchData.renderEmployeesTable(this.state.employees);

    return (
      <div>
        <h1>Employees</h1>
        <p>This component demonstrates fetching data from the server with an API that returns employees information.</p>
        {contents}
      </div>
    );
  }
}
