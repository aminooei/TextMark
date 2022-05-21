# TextMark

TextMark is an open source text annotation tool for humans. It provides annotation features for text classification, and text annotation tasks. So, you can create labeled data for sentiment analysis, named entity recognition, text summarization and so on. Just create a project, upload data and start annotating. You can build a dataset in hours.

### Docker Demo

You can download and try the annotation demo on Docker on your PC. To install it on your PC, follow the step in bellow: 

You need to install Git and to clone the repository:

```bash
git clone https://github.com/aminooei/TextMark
cd TextMark
docker-compose up
```
After running the above mentioned commands, access <http://localhost:8080/>.


You may also download and run this demo project locally on your PC in Microsoft Visual Studio 2019, OR just run it in IIS.

```bash
To run this project in Docker, you need to edit the appsettings.json file and uncomment the "Connection String for Docker".
To run this project locally on your PC IIS, you need to edit the appsettings.json file and uncomment the "Connection String for PC".
```

## Documentation

To read the user manual, first you need to run the project and login to the Admin section by using the below information: 
[Username: Admin] ---- [Password: 12345]. 


## Contribution

As with any software, TextMark is under continuous development. If you have requests for features, please file an issue describing your request. Also, if you want to see work towards a specific feature, feel free to contribute by working towards it. The standard procedure is to fork the repository, add a feature, fix a bug, then file a pull request that your changes are to be merged into the main repository and included in the next release.



## Citation

```tex
@misc{TextMark,
  title={{TextMark}: Text Annotation Tool for Human},
  url={https://github.com/aminooei/TextMark},
  note={Software available from https://github.com/aminooei/TextMark},
  author={
    Amin Minouei},
  year={2022},
}
```

## Contact

For help and feedback, please feel free to contact [the author](https://github.com/aminooei).
