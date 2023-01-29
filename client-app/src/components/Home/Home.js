import React, { useState } from 'react';
import WordLayout from "../Layout"
// import { useNavigate } from "react-router-dom";
// import {
//     Row, Col, Typography, Input, Form, Button, Checkbox,
//     Radio, Switch, Slider, Select, message
// } from 'antd';

import "./styles.css";
// import { Components } from 'antd/lib/date-picker/generatePicker';

function Home() {
    // State with list of all checked item
    const [checked, setChecked] = useState([]);
    const checkListSiemens = ["PKI Card Temini", "PKI Tanimlama", "EHS Egitimi", "Telefon talebi", "Laptop", "AZM Doldurma"];
    const checkListProject = ["Development ortaminin kurulumu", "Takim arkadaslarinla tanisma", "Firewall talepleri"]

    // Add/Remove checked item from list
    const handleCheck = (event) => {
        var updatedList = [...checked];
        if (event.target.checked) {
            updatedList = [...checked, event.target.value];
        } else {
            updatedList.splice(checked.indexOf(event.target.value), 1);
        }
        setChecked(updatedList);
    };

    // const ranking = require("../../db.json").members[0].ranking;

    // Return classes based on whether item is checked
    var isChecked = (item) =>
        checked.includes(item) ? "checked-item" : "not-checked-item";

    return (
        <WordLayout>
        <div className="home">
            <div class="notification-list">
                <div class="ntfc-box ntfc-siemens">
                    <div class="ntfc-info">
                        <h2>Today</h2>
                        <h2>08:00</h2>
                        <h2 class="id">ID: #4</h2>
                    </div>
                    <h3>Training</h3>
                    <p class="ntfc-description">You have an upcoming EHS training.</p>
                    <p class="ntfc-description">Navigate to the calendar to check the date of your training.</p>
                </div>

                <div class="ntfc-box ntfc-siemens">
                    <div class="ntfc-info">
                        <h2>Yesterday</h2>
                        <h2>12:45</h2>
                        <h2 class="id">ID: #3</h2>
                    </div>
                    <h3>Congrats!</h3>
                    <p class="ntfc-description">Great to see that Aysenur's help solved your problem.</p>
                    <p class="ntfc-description">Do not forget to recognize your friends by acknowledging their help.</p>
                </div>

                <div class="ntfc-box">
                    <div class="ntfc-info">
                        <h2>Today</h2>
                        <h2>12:45 – 12:55</h2>
                        <h2 class="id">ID: #1</h2>
                    </div>
                    <h3>Headline</h3>
                    <p class="ntfc-description">This is a notification. Hover over to expand.</p>
                    <p class="ntfc-description">Fill in this part with details. See for additional information.</p>
                </div>
            </div>
            <div className="checkList">
                <div className="title">Siemens On Boarding:</div>
                <div className="list-container">
                    {checkListSiemens.map((item, index) => (
                        <div key={index}>
                            <input value={item} type="checkbox" onChange={handleCheck} />
                            <span className={isChecked(item)}>{item}</span>
                        </div>
                    ))}
                </div>
                <div className="title">Project On Boarding:</div>
                <div className="list-container">
                    {checkListProject.map((item, index) => (
                        <div key={index}>
                            <input value={item} type="checkbox" onChange={handleCheck} />
                            <span className={isChecked(item)}>{item}</span>
                        </div>
                    ))}
                </div>
                <div>
                    <label for="file">Onboarding progress:</label>
                    <progress class="progress" id="file" value={checked.length} max={checkListSiemens.length + checkListProject.length}> 32% </progress>
                </div>
                <div>
                    <label for="file">How close you are to 50 stars? </label>
                    <progress class="progress" id="file" value={0} max="50"> 32% </progress>
                </div>
            </div>
        </div>
        </WordLayout>
    );
}

export default Home;
