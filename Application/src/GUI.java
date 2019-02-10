import javax.swing.*;
import java.awt.*;

public class GUI {
    private JPanel mainPanel;

    public static void main(String[] args) {
        JFrame mainFrame = new JFrame("FSV");
        mainFrame.setContentPane(new GUI().mainPanel);
        mainFrame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        mainFrame.pack();
        mainFrame.setResizable(false);
        mainFrame.setMaximumSize(new Dimension(600,800));
        mainFrame.setVisible(true);
    }
}
