import React, { Component } from 'react';

class Clock extends Component {
    constructor(props) {
        super(props);
        this.state = {
            date: new Date(),
        }
    }

    componentDidMount() {
        this.timer = setInterval(() => this.onTick(), 1000);
    }
    
    componentWillUnmount() {
        clearInterval(this.timer);
    }

    onTick () {
        this.setState ({
            date: new Date(),
        })
    }

    render () {
        return (
            <div>
                <div>Clock</div>
                <h2>It is {this.state.date.toLocaleTimeString()}.</h2>
            </div>
            );
    }
}

export default Clock;