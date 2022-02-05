import React from 'react';
import { Button, Divider, Header, Icon, Segment } from 'semantic-ui-react';
import { PodItemCreator } from '../PodItemCreator.jsx';

// Top level component -- will hold list of the pods
export default class ProductionLineCreate extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            pods: []
        };

        this.startCreatingPod = this.startCreatingPod.bind(this);
    }

    startCreatingPod() {
        this.setState((state) => ({
            pods: [
                { id: state.pods.length + 1 },
                ...state.pods
            ]
        }));
    }

    render() {
        const { pods } = this.state;

        return (
            <div className="production-line-create">
                <Header as="h1">Set up a new Production Line</Header>
                <Divider horizontal>
                    <Header as="h2">Pods</Header>
                </Divider>

                {pods.map((item) =>
                    <Segment
                        key={item.id}
                        padded
                        color="blue"
                    >
                        <PodItemCreator />
                    </Segment>
                )}

                <Segment color="blue" padded="very" textAlign="center" raised>
                    <Header>Add New Pod</Header>
                    <Segment.Inline>
                        <Button primary icon onClick={this.startCreatingPod}>
                            Add
                            <Icon name='add' />
                        </Button>
                    </Segment.Inline>
                </Segment>
            </div>
        );
    }
}

