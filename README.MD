<div align="center">
  <img src="https://raw.githubusercontent.com/doccano/doccano/master/docs/images/logo/doccano.png">
</div>

# TextMark

TextMark is an open source text annotation tool for humans. It provides annotation features for text classification, and text annotation tasks. So, you can create labeled data for sentiment analysis, named entity recognition, text summarization and so on. Just create a project, upload data and start annotating. You can build a dataset in hours.

## Demo

You can download and try the annotation demo on Docker on your PC. To install it on your Pc, just run the "docker-Compose up" command and it will automatically download all the requirments and their dependencies. 

![Demo image](https://raw.githubusercontent.com/doccano/doccano/master/docs/images/demo/demo.gif)

## Documentation

To read the user manual, login to the Admin section by using the below information: 
Username: Admin 
Password: 12345


### Docker Compose

You need to install Git and to clone the repository:

```bash
git clone https://github.com/doccano/doccano.git
cd doccano
```

_Note for Windows developers:_ Be sure to configure git to correctly handle line endings or you may encounter `status code 127` errors while running the services in future steps. Running with the git config options below will ensure your git directory correctly handles line endings.

```bash
git clone https://github.com/doccano/doccano.git --config core.autocrlf=input
```

Then, create an `.env` file with variables in the following format (see [./docker/.env.example](https://github.com/doccano/doccano/blob/master/docker/.env.example)):

```plain
# platform settings
ADMIN_USERNAME=admin
ADMIN_PASSWORD=password
ADMIN_EMAIL=admin@example.com
# rabbit mq settings
RABBITMQ_DEFAULT_USER=doccano
RABBITMQ_DEFAULT_PASS=doccano
# database settings
POSTGRES_USER=doccano
POSTGRES_PASSWORD=doccano
POSTGRES_DB=doccano
```

After running the following command, access <http://127.0.0.1/>.

```bash
docker-compose -f docker/docker-compose.prod.yml --env-file .env up
```

### One-click Deployment

| Service | Button |
|---------|---|
| AWS[^1]   | [![AWS CloudFormation Launch Stack SVG Button](https://cdn.rawgit.com/buildkite/cloudformation-launch-stack-button-svg/master/launch-stack.svg)](https://console.aws.amazon.com/cloudformation/home?#/stacks/new?stackName=doccano&templateURL=https://doccano.s3.amazonaws.com/public/cloudformation/template.aws.yaml)  |
| Heroku  | [![Deploy](https://www.herokucdn.com/deploy/button.svg)](https://dashboard.heroku.com/new?template=https%3A%2F%2Fgithub.com%2Fdoccano%2Fdoccano)  |
<!-- | GCP[^2] | [![GCP Cloud Run PNG Button](https://storage.googleapis.com/gweb-cloudblog-publish/images/run_on_google_cloud.max-300x300.png)](https://console.cloud.google.com/cloudshell/editor?shellonly=true&cloudshell_image=gcr.io/cloudrun/button&cloudshell_git_repo=https://github.com/doccano/doccano.git&cloudshell_git_branch=CloudRunButton)  | -->

> [^1]: (1) EC2 KeyPair cannot be created automatically, so make sure you have an existing EC2 KeyPair in one region. Or [create one yourself](https://docs.aws.amazon.com/AWSEC2/latest/UserGuide/ec2-key-pairs.html#having-ec2-create-your-key-pair). (2) If you want to access doccano via HTTPS in AWS, here is an [instruction](https://github.com/doccano/doccano/wiki/HTTPS-setting-for-doccano-in-AWS).
<!-- > [^2]: Although this is a very cheap option, it is only suitable for very small teams (up to 80 concurrent requests). Read more on [Cloud Run docs](https://cloud.google.com/run/docs/concepts). -->
## FAQ

- [How to create a user](https://doccano.github.io/doccano/faq/#how-to-create-a-user)
- [How to add a user to your project](https://doccano.github.io/doccano/faq/#how-to-add-a-user-to-your-project)
- [How to change the password](https://doccano.github.io/doccano/faq/#how-to-change-the-password)

See the [documentation](https://doccano.github.io/doccano/) for details.

## Contribution

As with any software, doccano is under continuous development. If you have requests for features, please file an issue describing your request. Also, if you want to see work towards a specific feature, feel free to contribute by working towards it. The standard procedure is to fork the repository, add a feature, fix a bug, then file a pull request that your changes are to be merged into the main repository and included in the next release.

Here are some tips might be helpful. [How to Contribute to Doccano Project](https://github.com/doccano/doccano/wiki/How-to-Contribute-to-Doccano-Project)

## Citation

```tex
@misc{doccano,
  title={{doccano}: Text Annotation Tool for Human},
  url={https://github.com/doccano/doccano},
  note={Software available from https://github.com/doccano/doccano},
  author={
    Hiroki Nakayama and
    Takahiro Kubo and
    Junya Kamura and
    Yasufumi Taniguchi and
    Xu Liang},
  year={2018},
}
```

## Contact

For help and feedback, please feel free to contact [the author](https://github.com/Hironsan).